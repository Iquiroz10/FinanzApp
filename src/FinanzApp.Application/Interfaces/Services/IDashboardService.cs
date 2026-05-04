using FinanzApp.Application.DTOs.Dashboard;

namespace FinanzApp.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<SummaryDto> GetSummaryAsync(Guid userId);
    Task<DebtOverviewDto> GetDebtOverviewAsync(Guid userId);
    Task<ExpenseAnalysisDto> GetExpenseAnalysisAsync(Guid userId);
    Task<AvailableBalanceDto> GetAvailableBalanceAsync(Guid userId);
}