using ClanChat.Application.Exceptions;
using ClanChat.Application.Requests;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    
    public DeleteUserCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new UserRequest(request.Name), cancellationToken).ConfigureAwait(false);
        
        if (user == null)
            throw new NotFoundException($"Clan {request.Name} was not found");

        _dbContext.Users.Remove(user);
        await _dbContext.SaveContextChangesAsync(cancellationToken);

        return Unit.Value;
    }
}