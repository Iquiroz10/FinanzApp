using FinanzApp.Application.DTOs.Investment;
using FluentValidation;

namespace FinanzApp.Application.Validators;

public class InvestmentCreateValidator : AbstractValidator<InvestmentCreateDto>
{
    public InvestmentCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Institution)
            .NotEmpty()
            .WithMessage("Institution is required")
            .MaximumLength(200)
            .WithMessage("Institution cannot exceed 200 characters");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required")
            .MaximumLength(100)
            .WithMessage("Country cannot exceed 100 characters");

        RuleFor(x => x.InvestedAmount)
            .GreaterThan(0)
            .WithMessage("Invested amount must be greater than zero");

        RuleFor(x => x.CurrentValue)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Current value cannot be negative");

        RuleFor(x => x.ReturnRate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Return rate cannot be negative");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required");

        RuleFor(x => x.MaturityDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("Maturity date must be after start date")
            .When(x => x.MaturityDate.HasValue);
    }
}