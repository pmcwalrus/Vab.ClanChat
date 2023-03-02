using System.Reflection;
using ClanChat.Application.Models;
using ClanChat.Application.Store;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Integration.EntityFramework;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
#pragma warning disable CS8618
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
#pragma warning restore CS8618
    {
    }

    public DbSet<Clan> Clans { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task SaveContextChangesAsync(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
}