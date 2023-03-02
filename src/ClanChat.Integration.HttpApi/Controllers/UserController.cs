using ClanChat.Application.Commands;
using ClanChat.Application.Models;
using ClanChat.Application.Requests;
using ClanChat.Integration.HttpApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClanChat.Integration.HttpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{name}", Name = "GetUser`")]
    public async Task<ActionResult<ClanDto>> Get(string name, CancellationToken cancellationToken)
    {
        var request = new UserRequest(name);
        var user = await _mediator.Send(request, cancellationToken);

        if (user == null)
            return NotFound();

        var dto = new UserDto
        {
            Name = user.Name,
            Clan = user.Clan?.Name
        };
        
        return Ok(dto);
    }
    
    [HttpGet(Name = "GetUserCollection")]
    public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetUserCollection(CancellationToken cancellationToken)
    {
        var request = new UserCollectionRequest();
        var users = await _mediator.Send(request, cancellationToken);

        var dtoCollection = users.Select(user => new UserDto()
        {
            Name = user.Name,
            Clan = user.Clan?.Name
        });
        
        return Ok(dtoCollection);
    }
    
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Create(NewUserDto newUserDto, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(newUserDto.Name);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpDelete("{name}", Name = "DeleteUser")]
    public async Task<ActionResult> Delete(string name, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(name);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut("{userName}", Name = "JoinToClan")]
    public async Task<ActionResult> JoinToClan(string userName, CancellationToken cancellationToken, string? clanName = null)
    {
        var command = new JoinToClanCommand(userName, clanName);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
}