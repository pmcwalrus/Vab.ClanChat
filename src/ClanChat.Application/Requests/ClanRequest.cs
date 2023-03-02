using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record ClanRequest(string Name) : IRequest<Clan?>;