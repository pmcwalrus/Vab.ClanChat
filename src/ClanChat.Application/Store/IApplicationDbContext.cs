using ClanChat.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Store;

public interface IApplicationDbContext
{
    DbSet<Clan> Clans { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<User> Users { get; set; }

    Task SaveContextChangesAsync(CancellationToken cancellationToken);
}