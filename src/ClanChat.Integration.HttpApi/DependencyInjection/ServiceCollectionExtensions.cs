using ClanChat.Integration.HttpApi.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ClanChat.Integration.HttpApi.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void UseHttpApi(this IServiceCollection services)
    {
        services.AddControllers(o => o.Filters.Add(new ApiExceptionFilterAttribute()));
    }
}