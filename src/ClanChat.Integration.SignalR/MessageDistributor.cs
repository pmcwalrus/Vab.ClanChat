using ClanChat.Application.Distribution;
using ClanChat.Integration.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ClanChat.Integration.SignalR;

internal sealed class MessageDistributor: IMessageDistributor
{
    private readonly IHubContext<ChatHub> _hubContext;
    
    public MessageDistributor(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(string userName, string clanName, string message, DateTimeOffset timestamp)
    {
        await _hubContext.Clients.Group(clanName).SendAsync("ReceiveMessage", userName, message, timestamp.ToString("g"));
    }
}