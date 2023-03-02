using ClanChat.Application.Models;
using ClanChat.Application.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClanChat.Application.Requests.Handlers;

internal sealed class UserRequestHandler : IRequestHandler<UserRequest, User?>
{
    private readonly IApplicationDbContext _dbContext;

    public UserRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> Handle(UserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Clan)
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken)
            .ConfigureAwait(false);

        return user;
    }
}