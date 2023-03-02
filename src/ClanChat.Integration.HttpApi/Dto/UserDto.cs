namespace ClanChat.Integration.HttpApi.Dto;

public record UserDto
{
    public string Name { get; set; } = default!;
    public string? Clan { get; set; }
}