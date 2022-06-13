using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAllUsers;

/// <summary>
/// Returns all users in the system
/// </summary>
public class GetAllUsersQuery : IRequest<List<UserDto>>
{
    
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _applicationDbContext.Users.Select(x => new UserDto()
        {
            Id = x.Id,
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            RoundCount = x.Rounds.Count,
            TotalScore = x.Rounds.Sum(y => y.Score)
        }).ToListAsync(cancellationToken);

        return users;

    }
}