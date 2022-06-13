using Application.Rounds.Commands.RecordRound;
using Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundsController : Controller
{
    private readonly IMediator _mediator;

    public RoundsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost(Name = "RecordRound")]
    public async Task<ActionResult<RoundDto>> RecordRound([FromBody] RecordRoundCommand command)
    {
        var response = await _mediator.Send(command);
        return response is not null ? Ok(response) : BadRequest("Could not record round");
    }
}