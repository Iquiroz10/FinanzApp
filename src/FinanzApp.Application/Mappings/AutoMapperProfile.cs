using AutoMapper;
using FinanzApp.Application.DTOs.Category;
using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Income
        CreateMap<IncomeCreateDto, Income>();
        CreateMap<Income, IncomeResponseDto>();

        // Expense
        CreateMap<ExpenseCreateDto, Expense>();
        CreateMap<Expense, ExpenseResponseDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));

        // Debt
        CreateMap<DebtCreateDto, Debt>();
        CreateMap<Debt, DebtResponseDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.RemainingPayments,
                opt => opt.MapFrom(src =>
                    src.MonthlyPayment > 0
                        ? (int)Math.Ceiling(src.RemainingBalance / src.MonthlyPayment)
                        : (int?)null));

        // Investment
        CreateMap<InvestmentCreateDto, Investment>();
        CreateMap<Investment, InvestmentResponseDto>()
            .ForMember(dest => dest.ProfitLoss,
                opt => opt.MapFrom(src => src.CurrentValue - src.InvestedAmount));

        // Category
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryResponseDto>();
    }
}