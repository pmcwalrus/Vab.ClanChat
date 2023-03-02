using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ClanChat.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
    }
    
}