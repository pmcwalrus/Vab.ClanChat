using ClanChat.Application.Exceptions;
using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class MessageCollectionRequestHandler : IRequestHandler<MessageCollectionRequest, IReadOnlyCollection<Message>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    public MessageCollectionRequestHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<IReadOnlyCollection<Message>> Handle(MessageCollectionRequest request, CancellationToken cancellationToken)
    {
        var clan = await _mediator.Send(new ClanRequest(request.ClanName), cancellationToken).ConfigureAwait(false);

        if (clan == null)
            throw new NotFoundException($"Clan {request.ClanName} was not found");
        
        var messages = await _dbContext.Messages
            .AsNoTracking()
            .Include(x => x.ToClan)
            .Include(x => x.FromUser)
            .Where(x => x.ToClanId == clan.Id)
            .OrderByDescending(x => x.Timestamp)
            .Take(request.Count)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        return messages;
    }
}