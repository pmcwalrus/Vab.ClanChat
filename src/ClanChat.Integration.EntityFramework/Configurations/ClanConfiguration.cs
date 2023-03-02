using ClanChat.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClanChat.Integration.EntityFramework.Configurations;

public class ClanConfiguration : IEntityTypeConfiguration<Clan>
{
    public void Configure(EntityTypeBuilder<Clan> builder)
    {
        builder.ToTable("Clans");

        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Name).IsUnique();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(50);

        builder.HasData(
            new Clan { Id = 1, Name = "King Gizzard" },
            new Clan { Id = 2, Name = "Lizard Wizard" },
            new Clan { Id = 3, Name = "Sigma Males"}
        );
    }
}