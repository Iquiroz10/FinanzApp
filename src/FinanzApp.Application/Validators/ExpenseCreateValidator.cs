using FinanzApp.Application.DTOs.Expense;
using FluentValidation;

namespace FinanzApp.Application.Validators;

public class ExpenseCreateValidator : AbstractValidator<ExpenseCreateDto>
{
    public ExpenseCreateValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Date cannot be in the future");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(300)
            .WithMessage("Description cannot exceed 300 characters");
    }
}