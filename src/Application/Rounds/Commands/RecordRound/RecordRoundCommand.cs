using Application.Interfaces;
using Application.Users.Queries.GetUser;
using GameScorer;
using MediatR;
using Round = Domain.Entities.Round;

namespace Application.Rounds.Commands.RecordRound;

public class RecordRoundCommand : IRequest<RoundDto>
{
    public Guid UserId { get; set; }
    public string InputString { get; set; }
}

public class RecordRoundCommandHandler : IRequestHandler<RecordRoundCommand, RoundDto>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public RecordRoundCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<RoundDto> Handle(RecordRoundCommand command, CancellationToken cancellationToken)
    {

        var scorer = new Scorer();
        var gameRound = scorer.ParseRound(command.InputString);

        var round = new Round()
        {
            UserId = command.UserId,
            InputString = command.InputString,
            RecordedAt = DateTime.Now,
            Ruleset = gameRound.Ruleset,
            Score = gameRound.Score
        };

        await _applicationDbContext.Rounds.AddAsync(round);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return new RoundDto()
        {
            Id = round.Id,
            Score = round.Score,
            InputString = round.InputString,
            RecordedAt = round.RecordedAt
        };


    }
}