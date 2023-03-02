using System.Net;
using System.Net.Mime;
using ClanChat.Application.DependencyInjection;
using ClanChat.Integration.EntityFramework.DependencyInjection;
using ClanChat.Integration.HttpApi.DependencyInjection;
using ClanChat.Integration.SignalR.DependencyInjection;
using ClanChat.Integration.SignalR.Hubs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var configuration = GetConfiguration(args);

var connectionString = configuration.GetValue<string>("connectionString");

try
{
    var options = new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
    };

    var builder = WebApplication.CreateBuilder(options);

    builder.Services.AddHealthChecks()
        .AddNpgSql(connectionString, name: "Postgres Db");

    builder.Services.AddHttpLogging(o=>
    {
        o.LoggingFields = HttpLoggingFields.All;
        o.MediaTypeOptions.AddText(MediaTypeNames.Application.Json);
    });

    var redisIp = IPAddress.Parse(configuration.GetValue<string>("Redis:Address"));
    var redisPort = configuration.GetValue<int>("Redis:Port");
    builder.Services.AddSignalR().AddStackExchangeRedis(o =>
    {
        o.ConnectionFactory = async writer =>
        {
            var config = new ConfigurationOptions
            {
                AbortOnConnectFail = false
            };
            
            config.EndPoints.Add(redisIp, redisPort);
            
            var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
            connection.ConnectionFailed += (_, e) =>
            {
                Console.Error.WriteLine("Connection to Redis failed.");
            };

            if (!connection.IsConnected)
            {
                Console.WriteLine("Did not connect to Redis.");
            }
            else
            {
                Console.WriteLine("Connected to Redis sucessfuly.");
            }

            return connection;
        };
    });
    
    ConfigureSwagger(builder);

    builder.Services.UseEntityFramework(o =>
    {
        o.UseNpgsql(connectionString);
        o.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
    });

    builder.Services.AddApplication();
    builder.Services.UseHttpApi();
    builder.Services.UseSignalR();
    builder.Services.AddCors();
    
    var app = builder.Build();

    app.UsePathBase(builder.Configuration.GetValue<string>("BasePath"));

    app.UseRouting();

    app.UseCors(b => b.SetIsOriginAllowed(_ => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
    
    app.UseForwardedHeaders(new ForwardedHeadersOptions {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    
    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<ChatHub>("/chat");
        endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = _ => true
        });
        endpoints.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            Predicate = h => h.Tags.Contains("ready")
        });
        endpoints.MapHealthChecks("/healthz/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });
    });

    await app.RunAsync().ConfigureAwait(false);

    return 0;
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
    return -1;
}

static void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(o =>
        {
            o.MapType(typeof(TimeSpan), () => new OpenApiSchema { Type = "string", Format = "time-span" });
            o.MapType(typeof(TimeSpan?), () => new OpenApiSchema { Type = "string", Format = "time-span" });
            o.MapType(typeof(decimal), () => new OpenApiSchema { Type = "number", Format = "decimal" });
            o.MapType(typeof(decimal?), () => new OpenApiSchema { Type = "number", Format = "decimal" });

            o.DescribeAllParametersInCamelCase();
            o.SupportNonNullableReferenceTypes();
            o.UseAllOfToExtendReferenceSchemas();

            o.SwaggerDoc("v1", new OpenApiInfo { Title = "Clan Chat Service", Version = "v1" });
        })
        .AddSwaggerGenNewtonsoftSupport();
}

static IConfiguration GetConfiguration(string[] args)
{
    return new ConfigurationBuilder()
        .SetBasePath(WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();
}
