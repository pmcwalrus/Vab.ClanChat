namespace ClanChat.Integration.HttpApi.Dto;

public record NewMessageDto
{
    public string Content { get; set; } = default!;
    public string ClanName { get; set; } = default!;
    public string SenderName { get; set; } = default!;
}