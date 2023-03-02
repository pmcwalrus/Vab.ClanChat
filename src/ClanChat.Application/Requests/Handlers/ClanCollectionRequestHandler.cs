using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class ClanCollectionRequestHandler : IRequestHandler<ClanCollectionRequest, IReadOnlyCollection<Clan>>
{
    private readonly IApplicationDbContext _dbContext;

    public ClanCollectionRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Clan>> Handle(ClanCollectionRequest request, CancellationToken cancellationToken)
    {
        var clan = await _dbContext.Clans
            .AsNoTracking()
            .Include(x => x.Users)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        return clan;
    }
}