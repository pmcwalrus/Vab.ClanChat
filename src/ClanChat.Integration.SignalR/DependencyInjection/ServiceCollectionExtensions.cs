using ClanChat.Application.Distribution;
using Microsoft.Extensions.DependencyInjection;

namespace ClanChat.Integration.SignalR.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void UseSignalR(this IServiceCollection services)
    {
        services.AddTransient<IMessageDistributor, MessageDistributor>();
    }
}