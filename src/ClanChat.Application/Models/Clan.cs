namespace ClanChat.Application.Models;

public class Clan
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Message> Messages { get; set; } = default!;
    public ICollection<User> Users { get; set; } = default!;
}