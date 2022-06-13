using FluentValidation;

namespace Application.Scorer.Queries.GetScore;

public class GetScoreQueryValidator : AbstractValidator<GetScoreQuery>
{
    public GetScoreQueryValidator()
    {
        RuleFor(x => x.InputString)
            .NotEmpty().WithMessage("Input string is required");
        
        RuleFor(v => v.InputString)
            .Must(inputString =>
            {
                try
                {
                    var gameScorer = new GameScorer.Scorer();
                    gameScorer.PlayRound(inputString);
                    return true;
                }
                catch
                {
                    return false;
                }
            }).WithMessage("InputString must be in the expected format");
    }
}