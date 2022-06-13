using FluentValidation;
using GameScorer;

namespace Application.Rounds.Commands.RecordRound;

public class RecordRoundCommandValidator : AbstractValidator<RecordRoundCommand>
{

    public RecordRoundCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(v => v.InputString)
            .NotEmpty().WithMessage("InputString is required");

        RuleFor(v => v.InputString)
            .Must(inputString =>
            {
                try
                {
                    var gameScorer = new Scorer();
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