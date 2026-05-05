using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Domain.Enums;
using FluentValidation;

namespace FinanzApp.Application.Validators;

public class DebtCreateValidator : AbstractValidator<DebtCreateDto>
{
    public DebtCreateValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero");

        RuleFor(x => x.RemainingBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Remaining balance cannot be negative")
            .LessThanOrEqualTo(x => x.TotalAmount)
            .WithMessage("Remaining balance cannot exceed total amount");

        RuleFor(x => x.MonthlyPayment)
            .GreaterThan(0)
            .WithMessage("Monthly payment must be greater than zero");

        RuleFor(x => x.InterestRate)
            .InclusiveBetween(0, 100)
            .WithMessage("Interest rate must be between 0 and 100");

        // Reglas específicas para tarjetas de crédito
        When(x => x.DebtType == DebtType.CreditCard, () =>
        {
            RuleFor(x => x.CreditLimit)
                .NotNull()
                .WithMessage("Credit limit is required for credit cards")
                .GreaterThan(0)
                .WithMessage("Credit limit must be greater than zero");

            RuleFor(x => x.CutoffDate)
                .NotNull()
                .WithMessage("Cutoff date is required for credit cards");
        });

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date")
            .When(x => x.EndDate.HasValue);
    }
}