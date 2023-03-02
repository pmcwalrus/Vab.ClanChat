namespace ClanChat.Application.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? ClanId { get; set; } = default!;
    public Clan? Clan { get; set; }
    public ICollection<Message> Messages { get; set; } = default!;
}