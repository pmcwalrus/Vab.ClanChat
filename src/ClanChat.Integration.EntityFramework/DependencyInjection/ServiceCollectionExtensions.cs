using ClanChat.Application.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClanChat.Integration.EntityFramework.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void UseEntityFramework(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> configure)
    {
        serviceCollection.AddDbContext<IApplicationDbContext, ApplicationDbContext>(configure);
    }   
}