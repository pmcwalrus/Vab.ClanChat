using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record MessageCollectionRequest(string ClanName, int Count) : IRequest<IReadOnlyCollection<Message>>;