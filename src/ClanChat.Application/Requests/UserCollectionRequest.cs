using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record UserCollectionRequest() : IRequest<IReadOnlyCollection<User>>;