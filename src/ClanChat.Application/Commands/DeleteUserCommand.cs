using MediatR;

namespace ClanChat.Application.Commands;

public record DeleteUserCommand(string Name) : IRequest;