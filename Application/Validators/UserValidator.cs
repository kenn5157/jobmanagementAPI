using Domain;
using FluentValidation;

namespace Application;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleSet("Default", () =>
        {
            RuleFor(u => u.userID).GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(u => u.userID).NotNull().WithMessage("Id cannot be null");
            RuleFor(u => u.email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.email).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
        });
        
        
        RuleSet("Add", () =>
        { 
            RuleFor(u => u.userID).GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(u => u.userID).NotNull().WithMessage("Id cannot be null");
            RuleFor(u => u.email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.email).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
        });
        
    }
    
    
}