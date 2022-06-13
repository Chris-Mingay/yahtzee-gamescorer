using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserDto = Application.Users.Queries.GetAllUsers.UserDto;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var query = new GetUserQuery
        {
            UserId = id
        };
        var response = await _mediator.Send(query);
        return response is not null ? Ok(response) : BadRequest($"User '{id}' not found");
    }
}