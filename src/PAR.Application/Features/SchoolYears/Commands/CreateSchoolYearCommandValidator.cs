﻿using FluentValidation;

namespace PAR.Application.Features.SchoolYears.Commands;

public class CreateSchoolYearCommandValidator : AbstractValidator<CreateSchoolYearCommand>
{
    public CreateSchoolYearCommandValidator()
    {
        RuleFor(x => x.SchoolYearRequest.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");
        
        RuleFor(x => x.SchoolYearRequest.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.");
        
        RuleFor(x => x.SchoolYearRequest.StartDate)
            .LessThan(x => x.SchoolYearRequest.EndDate)
            .WithMessage("Start date must be less than end date.");
    }
}