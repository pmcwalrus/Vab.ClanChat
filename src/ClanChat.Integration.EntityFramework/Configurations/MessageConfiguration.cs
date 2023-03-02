using ClanChat.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClanChat.Integration.EntityFramework.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Content).IsRequired().HasMaxLength(500);

        builder.HasOne(s => s.FromUser)
            .WithMany(m => m.Messages)
            .HasForeignKey(s => s.FromUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s => s.ToClan)
            .WithMany(m => m.Messages)
            .HasForeignKey(s => s.ToClanId)
            .OnDelete(DeleteBehavior.Cascade);

        var defaultMessages = new List<Message>();
        for (var i = 1; i < 200; ++i)
        {
            var clanId = i % 2 + 1;
            var message = new Message
            {
                Id = i,
                Content = $"Default message #{i}",
                FromUserId = clanId * 10 + i % 10,
                ToClanId = clanId,
                Timestamp = DateTimeOffset.Now + TimeSpan.FromSeconds(i),
            };
            defaultMessages.Add(message);
        }

        builder.HasData(defaultMessages);
    }
}