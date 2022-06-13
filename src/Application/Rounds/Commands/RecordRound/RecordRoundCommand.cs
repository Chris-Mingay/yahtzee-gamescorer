using Application.Interfaces;
using Application.Rounds.Events;
using Application.Users.Queries.GetUser;
using GameScorer;
using MediatR;
using Round = Domain.Entities.Round;

namespace Application.Rounds.Commands.RecordRound;

/// <summary>
/// Record a round of yahtzee against the provided user
/// </summary>
public class RecordRoundCommand : IRequest<RoundDto>
{
    /// <summary>
    /// The user id
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// A yahtzee round in string format e.g. "(1, 1, 1, 1, 1) ones"
    /// </summary>
    public string InputString { get; set; }
}

public class RecordRoundCommandHandler : IRequestHandler<RecordRoundCommand, RoundDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMediator _mediator;

    public RecordRoundCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
    }
    
    public async Task<RoundDto> Handle(RecordRoundCommand command, CancellationToken cancellationToken)
    {

        var scorer = new GameScorer.Scorer();
        var gameRound = scorer.ParseRound(command.InputString);

        var round = new Round()
        {
            UserId = command.UserId,
            InputString = command.InputString,
            RecordedAt = DateTime.Now,
            Ruleset = gameRound.Ruleset,
            Score = gameRound.Score
        };

        await _applicationDbContext.Rounds.AddAsync(round, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        if (round.Score == 30)
        {
            await _mediator.Publish(new MaxScoreEvent(round.Id), cancellationToken);
        }

        return new RoundDto()
        {
            Id = round.Id,
            Score = round.Score,
            InputString = round.InputString,
            RecordedAt = round.RecordedAt
        };


    }
}