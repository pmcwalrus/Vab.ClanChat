using MediatR;

namespace ClanChat.Application.Commands;

public record CreateClanCommand(string Name) : IRequest;