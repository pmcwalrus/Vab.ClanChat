namespace ClanChat.Integration.HttpApi.Dto;

public record NewClanDto
{
    public string Name { get; set; } = default!;
}