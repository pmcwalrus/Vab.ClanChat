using ClanChat.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClanChat.Integration.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Name).IsUnique();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(s => s.Clan)
            .WithMany(m => m.Users)
            .HasForeignKey(s => s.ClanId)
            .OnDelete(DeleteBehavior.SetNull);

        var defaultUsers = new List<User>();
        for (var i = 1; i < 40; ++i)
        {
            var clanId = i/10;
            var user = new User
            {
                Id = i,
                ClanId = clanId == 0 ? null : clanId,
                Name = $"User{i}"
            };
            defaultUsers.Add(user);
        }

        builder.HasData(defaultUsers);
    }
}