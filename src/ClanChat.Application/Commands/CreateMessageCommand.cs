using MediatR;

namespace ClanChat.Application.Commands;

public record CreateMessageCommand(string Content, string SenderName, string ClanName) : IRequest;