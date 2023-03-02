using ClanChat.Application.Models;
using MediatR;

namespace ClanChat.Application.Requests;

public record MessageRequest(int Id) : IRequest<Message?>;