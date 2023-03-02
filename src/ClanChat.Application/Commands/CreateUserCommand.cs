using MediatR;

namespace ClanChat.Application.Commands;

public record CreateUserCommand(string Name) : IRequest;