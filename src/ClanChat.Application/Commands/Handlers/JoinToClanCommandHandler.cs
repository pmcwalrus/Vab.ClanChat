using ClanChat.Application.Exceptions;
using ClanChat.Application.Models;
using ClanChat.Application.Requests;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class JoinToClanCommandHandler : IRequestHandler<JoinToClanCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    
    public JoinToClanCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(JoinToClanCommand request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new UserRequest(request.UserName), cancellationToken).ConfigureAwait(false);

        if (user == null)
            throw new NotFoundException($"User {request.UserName} was not found");

        if (request.ClanName != null)
        {
            var clan = await _mediator.Send(new ClanRequest(request.ClanName), cancellationToken).ConfigureAwait(false);
            
            if (clan == null)
                throw new NotFoundException($"Clan {request.ClanName} was not found");

            user.Clan = clan;
        }
        else
        {
            user.Clan = null;
        }

        await _dbContext.SaveContextChangesAsync(cancellationToken);

        return Unit.Value;
    }
}