using ClanChat.Application.Commands;
using ClanChat.Application.Models;
using ClanChat.Application.Requests;
using ClanChat.Integration.HttpApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClanChat.Integration.HttpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}", Name = "GetMessage")]
    public async Task<ActionResult<MessageDto>> Get(int id, CancellationToken cancellationToken)
    {
        var request = new MessageRequest(id);
        var message = await _mediator.Send(request, cancellationToken);

        if (message == null)
            return NotFound();

        var dto = new MessageDto
        {
            Content = message.Content,
            SenderName = message.FromUser.Name,
            ClanName = message.ToClan.Name,
            Timestamp = message.Timestamp
        };
        
        return Ok(dto);
    }
    
    [HttpGet("clan={clanName}&count={count}", Name = "GetMessageCollection")]
    public async Task<ActionResult<IReadOnlyCollection<MessageDto>>> Get(string clanName, int count, CancellationToken cancellationToken)
    {
        var request = new MessageCollectionRequest(clanName, count);
        var messages = await _mediator.Send(request, cancellationToken);

        var dtoCollection = messages.Select(message => new MessageDto
        {
            Content = message.Content,
            SenderName = message.FromUser.Name,
            ClanName = message.ToClan.Name,
            Timestamp = message.Timestamp
        });
        
        return Ok(dtoCollection);
    }

    [HttpPost(Name = "CreateMessage")]
    public async Task<ActionResult> Create(NewMessageDto newMessageDto, CancellationToken cancellationToken)
    {
        var command = new CreateMessageCommand(newMessageDto.Content, newMessageDto.SenderName, newMessageDto.ClanName);
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
}