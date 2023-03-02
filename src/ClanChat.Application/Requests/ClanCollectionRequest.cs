using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record ClanCollectionRequest : IRequest<IReadOnlyCollection<Clan>>;