using FinanzApp.Application.DTOs.Category;
using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Income
        config.NewConfig<IncomeCreateDto, Income>();
        config.NewConfig<Income, IncomeResponseDto>();

        // Expense
        config.NewConfig<ExpenseCreateDto, Expense>();
        config.NewConfig<Expense, ExpenseResponseDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        // Debt
        config.NewConfig<DebtCreateDto, Debt>();
        config.NewConfig<Debt, DebtResponseDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.RemainingPayments, src =>
                src.MonthlyPayment > 0
                    ? (int?)Math.Ceiling(src.RemainingBalance / src.MonthlyPayment)
                    : null);

        // Investment
        config.NewConfig<InvestmentCreateDto, Investment>();
        config.NewConfig<Investment, InvestmentResponseDto>()
            .Map(dest => dest.ProfitLoss, src => src.CurrentValue - src.InvestedAmount);

        // Category
        config.NewConfig<CategoryCreateDto, Category>();
        config.NewConfig<Category, CategoryResponseDto>();
    }
}