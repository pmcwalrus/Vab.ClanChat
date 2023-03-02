namespace ClanChat.Integration.HttpApi.Dto;

public record ClanDto
{
    public string Name { get; set; } = default!;
    public IReadOnlyCollection<string> Members { get; set; } = default!;
}