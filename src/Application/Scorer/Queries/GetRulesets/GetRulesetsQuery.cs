using MediatR;

namespace Application.Scorer.Queries.GetRulesets;

/// <summary>
/// Return a list of all rulesets in this current build
/// </summary>
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