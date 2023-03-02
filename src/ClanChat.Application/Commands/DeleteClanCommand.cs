using MediatR;

namespace ClanChat.Application.Commands;

public record DeleteClanCommand(string Name) : IRequest;