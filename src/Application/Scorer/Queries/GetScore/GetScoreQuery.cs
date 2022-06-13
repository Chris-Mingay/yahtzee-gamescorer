using MediatR;

namespace Application.Scorer.Queries.GetScore;

/// <summary>
/// Returns the score based on the incoming input string
/// </summary>
public class GetScoreQuery : IRequest<int>
{
    /// <summary>
    /// String representation of a yahtzee game e.g. "(1, 1, 1, 1, 1) ones" 
    /// </summary>
    public string InputString { get; set; }
}

public class GetScoreQueryHandler : IRequestHandler<GetScoreQuery, int>
{
    public Task<int> Handle(GetScoreQuery query, CancellationToken cancellationToken)
    {
        var scorer = new GameScorer.Scorer();
        var round = scorer.ParseRound(query.InputString);
        return Task.FromResult(round.Score);
    }
}
