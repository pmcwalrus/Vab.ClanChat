namespace ClanChat.Integration.HttpApi.Dto;

public record MessageDto
{
    public string Content { get; set; } = default!;
    public string ClanName { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public DateTimeOffset Timestamp { get; set; }
}