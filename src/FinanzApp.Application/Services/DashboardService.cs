using FinanzApp.Application.DTOs.Dashboard;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Enums;

namespace FinanzApp.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IDebtRepository _debtRepository;
    private readonly IInvestmentRepository _investmentRepository;

    public DashboardService(
        IIncomeRepository incomeRepository,
        IExpenseRepository expenseRepository,
        IDebtRepository debtRepository,
        IInvestmentRepository investmentRepository)
    {
        _incomeRepository = incomeRepository;
        _expenseRepository = expenseRepository;
        _debtRepository = debtRepository;
        _investmentRepository = investmentRepository;
    }

    public async Task<SummaryDto> GetSummaryAsync(Guid userId)
    {
        var incomes = await _incomeRepository.GetAllByUserAsync(userId);
        var expenses = await _expenseRepository.GetAllByUserAsync(userId);
        var debts = await _debtRepository.GetAllByUserAsync(userId);
        var investments = await _investmentRepository.GetAllByUserAsync(userId);

        // Quincena actual
        var now = DateTime.UtcNow;
        var periodStart = now.Day <= 15
            ? new DateTime(now.Year, now.Month, 1)
            : new DateTime(now.Year, now.Month, 16);
        var periodEnd = now.Day <= 15
            ? new DateTime(now.Year, now.Month, 15)
            : new DateTime(now.Year, now.Month,
                DateTime.DaysInMonth(now.Year, now.Month));

        var periodIncomes = incomes
            .Where(i => i.ReceivedDate >= periodStart && i.ReceivedDate <= periodEnd);

        var periodExpenses = expenses
            .Where(e => e.Date >= periodStart && e.Date <= periodEnd);

        var totalIncome = periodIncomes.Sum(i => i.Amount);
        var totalExpenses = periodExpenses.Sum(e => e.Amount);
        var totalDebt = debts.Sum(d => d.RemainingBalance);
        var totalMonthlyPayments = debts.Sum(d => d.MonthlyPayment);
        var totalInvested = investments.Sum(i => i.InvestedAmount);

        return new SummaryDto
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            TotalDebt = totalDebt,
            TotalInvested = totalInvested,
            AvailableBalance = totalIncome - totalExpenses - totalMonthlyPayments,
            DebtToIncomeRatio = totalIncome > 0
                ? Math.Round(totalDebt / totalIncome * 100, 2)
                : 0,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd
        };
    }

    public async Task<DebtOverviewDto> GetDebtOverviewAsync(Guid userId)
    {
        var debts = await _debtRepository.GetAllByUserAsync(userId);
        var debtList = debts.ToList();

        var debtDetails = debtList.Select(d =>
        {
            var usagePercentage = d.DebtType == DebtType.CreditCard && d.CreditLimit > 0
                ? Math.Round(d.RemainingBalance / d.CreditLimit!.Value * 100, 2)
                : (decimal?)null;

            var remainingPayments = d.MonthlyPayment > 0
                ? (int?)Math.Ceiling(d.RemainingBalance / d.MonthlyPayment)
                : null;

            return new DebtDetailDto
            {
                Id = d.Id,
                Name = d.Name,
                DebtType = d.DebtType.ToString(),
                RemainingBalance = d.RemainingBalance,
                MonthlyPayment = d.MonthlyPayment,
                CreditLimit = d.CreditLimit,
                UsagePercentage = usagePercentage,
                RemainingPayments = remainingPayments,
                // Tarjeta a evitar si supera el 80% de uso
                ShouldAvoid = usagePercentage >= 80
            };
        }).ToList();

        return new DebtOverviewDto
        {
            TotalRemainingDebt = debtList.Sum(d => d.RemainingBalance),
            TotalMonthlyPayments = debtList.Sum(d => d.MonthlyPayment),
            Debts = debtDetails,
            CreditCardsToAvoid = debtDetails.Where(d => d.ShouldAvoid).ToList()
        };
    }

    public async Task<ExpenseAnalysisDto> GetExpenseAnalysisAsync(Guid userId)
    {
        var expenses = await _expenseRepository.GetAllByUserAsync(userId);
        var expenseList = expenses.ToList();

        var totalExpenses = expenseList.Sum(e => e.Amount);

        var byCategory = expenseList
            .GroupBy(e => e.Category.Name)
            .Select(g => new ExpenseByCategoryDto
            {
                CategoryName = g.Key,
                Total = g.Sum(e => e.Amount),
                Percentage = totalExpenses > 0
                    ? Math.Round(g.Sum(e => e.Amount) / totalExpenses * 100, 2)
                    : 0,
                Count = g.Count()
            })
            .OrderByDescending(c => c.Total)
            .ToList();

        var recurringExpenses = expenseList
            .Where(e => e.IsRecurring)
            .Select(e => new RecurringExpenseDto
            {
                Description = e.Description,
                CategoryName = e.Category.Name,
                Amount = e.Amount
            })
            .ToList();

        return new ExpenseAnalysisDto
        {
            TotalExpenses = totalExpenses,
            ByCategory = byCategory,
            // Top 3 categorías donde más se gasta
            TopCategories = byCategory.Take(3).ToList(),
            RecurringExpenses = recurringExpenses
        };
    }

    public async Task<AvailableBalanceDto> GetAvailableBalanceAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        var periodStart = now.Day <= 15
            ? new DateTime(now.Year, now.Month, 1)
            : new DateTime(now.Year, now.Month, 16);
        var periodEnd = now.Day <= 15
            ? new DateTime(now.Year, now.Month, 15)
            : new DateTime(now.Year, now.Month,
                DateTime.DaysInMonth(now.Year, now.Month));

        var incomes = await _incomeRepository.GetAllByUserAsync(userId);
        var expenses = await _expenseRepository.GetAllByUserAsync(userId);
        var debts = await _debtRepository.GetAllByUserAsync(userId);
        var investments = await _investmentRepository.GetAllByUserAsync(userId);

        var periodIncome = incomes
            .Where(i => i.ReceivedDate >= periodStart && i.ReceivedDate <= periodEnd)
            .Sum(i => i.Amount);

        var periodExpenses = expenses
            .Where(e => e.Date >= periodStart && e.Date <= periodEnd)
            .Sum(e => e.Amount);

        var totalDebtPayments = debts.Sum(d => d.MonthlyPayment);

        var periodInvested = investments
            .Where(i => i.StartDate >= periodStart && i.StartDate <= periodEnd)
            .Sum(i => i.InvestedAmount);

        return new AvailableBalanceDto
        {
            Income = periodIncome,
            TotalExpenses = periodExpenses,
            TotalDebtPayments = totalDebtPayments,
            TotalInvested = periodInvested,
            Available = periodIncome - periodExpenses - totalDebtPayments - periodInvested,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd
        };
    }
}