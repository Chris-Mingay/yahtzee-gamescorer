using Application.Users.Commands.CreateUser;
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
    
    /// <summary>
    /// Returns an array of all users in the system
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    /// <summary>
    /// Gets the specified user including all associated round data
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<Application.Users.Queries.GetUser.UserDto>> GetUser(Guid id)
    {
        var query = new GetUserQuery
        {
            UserId = id
        };
        var response = await _mediator.Send(query);
        return response is not null ? Ok(response) : BadRequest($"User '{id}' not found");
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Ok(UserDto) if successful or BadRequest()</returns>
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response is not null ? Ok(response) : BadRequest("Could not create user");
    }
}