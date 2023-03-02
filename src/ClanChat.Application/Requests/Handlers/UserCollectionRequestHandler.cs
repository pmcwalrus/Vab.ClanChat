using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class UserCollectionRequestHandler : IRequestHandler<UserCollectionRequest, IReadOnlyCollection<User>>
{
    private readonly IApplicationDbContext _dbContext;

    public UserCollectionRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<User>> Handle(UserCollectionRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Clan)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        return user;
    }
}