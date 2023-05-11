using Domain;
using FluentValidation;

namespace Application;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleSet("Default", () =>
        {
            RuleFor(u => u.UserID).GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(u => u.UserID).NotNull().WithMessage("Id cannot be null");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Email).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
        });
        
        
        RuleSet("Add", () =>
        { 
            RuleFor(u => u.UserID).GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(u => u.UserID).NotNull().WithMessage("Id cannot be null");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Email).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
        });
        
    }
    
    
}