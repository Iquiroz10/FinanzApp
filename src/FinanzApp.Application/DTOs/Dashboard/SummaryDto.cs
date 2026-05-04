namespace FinanzApp.Application.DTOs.Dashboard;

public class SummaryDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalDebt { get; set; }
    public decimal TotalInvested { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal DebtToIncomeRatio { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}