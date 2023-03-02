using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class MessageRequestHandler : IRequestHandler<MessageRequest, Message?>
{
    private readonly IApplicationDbContext _dbContext;

    public MessageRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Message?> Handle(MessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _dbContext.Messages
            .Include(x => x.FromUser)
            .Include(x => x.ToClan)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        return message;
    }
}