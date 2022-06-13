using Application.Interfaces;
using Application.Users.Queries.GetAllUsers;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

/// <summary>
/// Creates a new user
/// </summary>
public class CreateUserCommand : IRequest<UserDto>
{
    /// <summary>
    /// The full name of the user [required]
    /// </summary>
    public string Fullname { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = command.Fullname,
            CreatedAt = DateTime.Now
        };
        await _applicationDbContext.Users.AddAsync(user, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            CreatedAt = user.CreatedAt
        };
    }
}