using ClanChat.Application.Commands;
using ClanChat.Application.Models;
using ClanChat.Application.Requests;
using ClanChat.Integration.HttpApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClanChat.Integration.HttpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClanController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{name}", Name = "GetClan")]
    public async Task<ActionResult<ClanDto>> Get(string name, CancellationToken cancellationToken)
    {
        var request = new ClanRequest(name);
        var clan = await _mediator.Send(request, cancellationToken);

        if (clan == null)
            return NotFound();

        var dto = new ClanDto
        {
            Name = clan.Name,
            Members = clan.Users.Select(x => x.Name).ToArray()
        };
        
        return Ok(dto);
    }
    
    [HttpGet(Name = "GetClanCollection")]
    public async Task<ActionResult<IReadOnlyCollection<ClanDto>>> GetClanCollection(CancellationToken cancellationToken)
    {
        var request = new ClanCollectionRequest();
        var clans = await _mediator.Send(request, cancellationToken);

        var dtoCollection = clans.Select(clan => new ClanDto
        {
            Name = clan.Name,
            Members = clan.Users.Select(x => x.Name).ToArray()
        });
        
        return Ok(dtoCollection);
    }
    
    [HttpPost(Name = "CreateClan")]
    public async Task<ActionResult> Create(NewClanDto newClanDto, CancellationToken cancellationToken)
    {
        var command = new CreateClanCommand(newClanDto.Name);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpDelete(Name = "DeleteClan")]
    public async Task<ActionResult> Delete(string name, CancellationToken cancellationToken)
    {
        var command = new DeleteClanCommand(name);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
}