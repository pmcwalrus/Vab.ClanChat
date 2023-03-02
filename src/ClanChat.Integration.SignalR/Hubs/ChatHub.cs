using ClanChat.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ClanChat.Integration.SignalR.Hubs;

public class ChatHub : Hub
{
    private static readonly Dictionary<string, string?> ConnectedGroups = new ();
    private const int LastMessagesCount = 50;
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinChat(string userName, string clanName, bool isReconnect)
    {
        var user = await _mediator.Send(new UserRequest(userName)).ConfigureAwait(false);

        if (user == null)
        {
            await Clients.Caller.SendAsync("OperationNotification", $"User {userName} does not exists");
            return;
        }

        if (user.Clan?.Name != clanName)
        {
            await Clients.Caller.SendAsync("OperationNotification", $"User {userName} is not a {clanName} clan member");
            return;
        }

        await JoinToClanChat(Context.ConnectionId, clanName);

        if (!isReconnect)
        {
            await Clients.Group(clanName).SendAsync("OperationNotification", $"User {userName} is connected to {clanName} clan chat");
            await SendLastClanMessages(clanName);
        }
    }

    private async Task JoinToClanChat(string connectionId, string clanName)
    {
        var currentGroup = ConnectedGroups[connectionId];
        if (currentGroup != null)
        {
            await Groups.RemoveFromGroupAsync(connectionId, currentGroup);
            ConnectedGroups[connectionId] = null;
        }
            
        await Groups.AddToGroupAsync(connectionId, clanName);
        ConnectedGroups[connectionId] = clanName;
    }
    
    private async Task SendLastClanMessages(string clanName)
    {
        var messages = await _mediator.Send(new MessageCollectionRequest(clanName, LastMessagesCount));
        foreach (var message in messages.OrderBy(x => x.Timestamp))
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message.FromUser.Name, message.Content, message.Timestamp.ToString("g"));
        }
    }
    
    public override Task OnConnectedAsync()
    {
        ConnectedGroups.Add(Context.ConnectionId, null);
        return base.OnConnectedAsync();
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectedGroups.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}