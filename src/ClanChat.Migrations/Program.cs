using ClanChat.Integration.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((ctx, s) =>
{
    s.AddDbContext<ApplicationDbContext>(db =>
    {
        var connectionString = ctx.Configuration.GetConnectionString(nameof(ApplicationDbContext));

        if (connectionString != null)
            db.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly.FullName));
        else
            db.UseNpgsql(x => x.MigrationsAssembly(typeof(Program).Assembly.FullName));
        db.EnableSensitiveDataLogging(ctx.HostingEnvironment.IsDevelopment());
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<ApplicationDbContext>();
if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}