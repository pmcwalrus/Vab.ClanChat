namespace ClanChat.Application.Distribution;

public interface IMessageDistributor
{
    Task SendMessage(string userName, string clanName, string message, DateTimeOffset timestamp);
}