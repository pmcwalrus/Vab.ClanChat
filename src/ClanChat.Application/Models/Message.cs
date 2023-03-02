namespace ClanChat.Application.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public DateTimeOffset Timestamp { get; set; }
    public int FromUserId { get; set; }
    public User FromUser { get; set; } = default!;
    public int ToClanId { get; set; }
    public Clan ToClan { get; set; } = default!;
}