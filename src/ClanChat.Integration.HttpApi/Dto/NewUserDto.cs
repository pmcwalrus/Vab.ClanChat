namespace ClanChat.Integration.HttpApi.Dto;

public record NewUserDto
{
    public string Name { get; set; } = default!;
}