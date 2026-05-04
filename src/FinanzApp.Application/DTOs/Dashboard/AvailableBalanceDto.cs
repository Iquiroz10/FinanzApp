namespace FinanzApp.Application.DTOs.Dashboard;

public class AvailableBalanceDto
{
    public decimal Income { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalDebtPayments { get; set; }
    public decimal TotalInvested { get; set; }
    public decimal Available { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}