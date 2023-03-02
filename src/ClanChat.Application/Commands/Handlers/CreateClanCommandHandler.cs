using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;

namespace ClanChat.Application.Commands.Handlers;

internal sealed class CreateClanCommandHandler : IRequestHandler<CreateClanCommand>
{
    private readonly IApplicationDbContext _dbContext;
    
    public CreateClanCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateClanCommand request, CancellationToken cancellationToken)
    {
        var clan = new Clan
        {
            Name = request.Name
        };

        _dbContext.Clans.Add(clan);
        await _dbContext.SaveContextChangesAsync(cancellationToken);

        return Unit.Value;
    }
}