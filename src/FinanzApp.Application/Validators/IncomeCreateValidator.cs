using FinanzApp.Application.DTOs.Income;
using FluentValidation;

namespace FinanzApp.Application.Validators;

public class IncomeCreateValidator : AbstractValidator<IncomeCreateDto>
{
    public IncomeCreateValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");

        RuleFor(x => x.ReceivedDate)
            .NotEmpty()
            .WithMessage("Received date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Received date cannot be in the future");

        RuleFor(x => x.PeriodStart)
            .NotEmpty()
            .WithMessage("Period start is required");

        RuleFor(x => x.PeriodEnd)
            .NotEmpty()
            .WithMessage("Period end is required")
            .GreaterThan(x => x.PeriodStart)
            .WithMessage("Period end must be after period start");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters");
    }
}