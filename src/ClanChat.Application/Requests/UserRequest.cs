using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record UserRequest(string Name) : IRequest<User?>;