using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname is required");
    }
}