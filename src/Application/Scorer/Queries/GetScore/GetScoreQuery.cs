using MediatR;

namespace Application.Scorer.Queries.GetScore;

public class GetScoreQuery : IRequest<int>
{
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
