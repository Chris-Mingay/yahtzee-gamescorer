using Application.Interfaces;
using Application.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetUserQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return _applicationDbContext.Users.Where(x=>x.Id == request.UserId)
            .Include(x => x.Rounds)
            .Select(x => new UserDto()
        {
            Id = x.Id,
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            RoundCount = x.Rounds.Count,
            TotalScore = x.Rounds.Sum(y => y.Score),
            Rounds = x.Rounds.Select(y => new RoundDto()
            {
                Id = y.Id,
                Score = y.Score,
                InputString = y.InputString,
                RecordedAt = y.RecordedAt
                
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken);
    }
}