namespace FinanzApp.Application.DTOs.Dashboard;

public class DebtOverviewDto
{
    public decimal TotalRemainingDebt { get; set; }
    public decimal TotalMonthlyPayments { get; set; }
    public List<DebtDetailDto> Debts { get; set; } = [];
    public List<DebtDetailDto> CreditCardsToAvoid { get; set; } = [];
}

public class DebtDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DebtType { get; set; } = string.Empty;
    public decimal RemainingBalance { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal? CreditLimit { get; set; }
    public decimal? UsagePercentage { get; set; }
    public int? RemainingPayments { get; set; }
    public bool ShouldAvoid { get; set; }
}