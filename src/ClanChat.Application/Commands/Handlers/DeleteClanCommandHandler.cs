using ClanChat.Application.Exceptions;
using ClanChat.Application.Requests;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class DeleteClanCommandHandler : IRequestHandler<DeleteClanCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    
    public DeleteClanCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(DeleteClanCommand request, CancellationToken cancellationToken)
    {
        var clan = await _mediator.Send(new ClanRequest(request.Name), cancellationToken).ConfigureAwait(false);
        
        if (clan == null)
            throw new NotFoundException($"Clan {request.Name} was not found");
        
        if (clan.Users.Count > 0)
             throw new ClanIsNotEmptyException($"Clan {request.Name} has active members");
        
        _dbContext.Clans.Remove(clan);
        await _dbContext.SaveContextChangesAsync(cancellationToken);

        return Unit.Value;
    }
}