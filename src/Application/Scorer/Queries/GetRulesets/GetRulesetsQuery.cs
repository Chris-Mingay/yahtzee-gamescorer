using MediatR;

namespace Application.Scorer.Queries.GetRulesets;

public class GetRulesetsQuery : IRequest<List<string>>
{
    
}

public class GetRulesetsQueryHandler : IRequestHandler<GetRulesetsQuery, List<string>>
{
    public Task<List<string>> Handle(GetRulesetsQuery query, CancellationToken cancellationToken)
    {
        var scorer = new GameScorer.Scorer();
        return Task.FromResult(scorer.GetRulesets());
    }
}