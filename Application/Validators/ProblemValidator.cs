﻿using Domain;
using FluentValidation;

namespace Application.Validators;

public class ProblemValidator : AbstractValidator<Problem>
{
    public ProblemValidator()
    {
        RuleSet("Default", () =>
        {
            RuleFor(x => x.ProblemId).GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.ProblemId).NotNull().WithMessage("Id cannot be null.");
            RuleFor(x => x.ProblemName).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.ProblemName).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
            RuleFor(x => x.Latitude).NotEmpty().WithMessage("Location cannot be empty.");
            RuleFor(x => x.Longitude).NotEmpty().WithMessage("Location cannot be empty.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Status).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Status may only contain alphanumeric characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image cannot be empty.");
        });

        RuleSet("Add", () =>
        {
            RuleFor(x => x.ProblemName).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.ProblemName).Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name may only contain alphanumeric characters.");
            RuleFor(x => x.Latitude).NotEmpty().WithMessage("Location cannot be empty.");
            RuleFor(x => x.Longitude).NotEmpty().WithMessage("Location cannot be empty.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status cannot be empty.");
            RuleFor(x => x.Status).Matches("^[a-zA-Z0-9 ]*$").WithMessage("status may only contain alphanumeric characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image cannot be empty.");
        });
    }
}
