using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IApplicationDbContext _dbContext;
    
    public CreateUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveContextChangesAsync(cancellationToken);

        return Unit.Value;
    }
}