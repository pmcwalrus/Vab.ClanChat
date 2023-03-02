using ClanChat.Application.Distribution;
using ClanChat.Application.Exceptions;
using ClanChat.Application.Models;
using ClanChat.Application.Requests;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IMessageDistributor _messageDistributor;
    
    public CreateMessageCommandHandler(IApplicationDbContext dbContext, 
        IMediator mediator, 
        IMessageDistributor messageDistributor)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _messageDistributor = messageDistributor;
    }
    
    public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var clan = await _mediator.Send(new ClanRequest(request.ClanName), cancellationToken).ConfigureAwait(false);
        
        if (clan == null)
            throw new NotFoundException($"Clan {request.ClanName} was not found");

        var user = await _mediator.Send(new UserRequest(request.SenderName), cancellationToken).ConfigureAwait(false);
        
        if (user == null)
            throw new NotFoundException($"User {request.SenderName} was not found");
        
        if (user.ClanId != clan.Id)
            throw new OperationIsForbiddenException($"User {user.Name} is not a {clan.Name} clan member");

        var message = new Message
        {
            Content = request.Content,
            FromUserId = user.Id,
            ToClanId = clan.Id,
            Timestamp = DateTimeOffset.Now.ToUniversalTime()
        };

        _dbContext.Messages.Add(message);
        await _dbContext.SaveContextChangesAsync(cancellationToken);

        await _messageDistributor.SendMessage(message.FromUser.Name, message.ToClan.Name, message.Content, message.Timestamp);
        
        return Unit.Value;
    }
}