using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class ClanRequestHandler : IRequestHandler<ClanRequest, Clan?>
{
    private readonly IApplicationDbContext _dbContext;

    public ClanRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Clan?> Handle(ClanRequest request, CancellationToken cancellationToken)
    {
        var clan = await _dbContext.Clans
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken)
            .ConfigureAwait(false);

        return clan;
    }
}