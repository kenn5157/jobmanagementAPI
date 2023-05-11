using Domain;
using FluentValidation;

namespace Applicatoin.Validators;

public class RegisterValidator : AbstractValidator<Register>
{
    public RegisterValidator()
    {
        RuleSet("Default", () =>
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).NotNull();
        });
    }
}