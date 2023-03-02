using MediatR;

namespace ClanChat.Application.Commands;

public record JoinToClanCommand(string UserName, string? ClanName) : IRequest;