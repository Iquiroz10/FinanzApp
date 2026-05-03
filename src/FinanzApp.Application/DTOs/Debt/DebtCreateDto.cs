using FinanzApp.Domain.Enums;

namespace FinanzApp.Application.DTOs.Debt;

public class DebtCreateDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DebtType DebtType { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal RemainingBalance { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal InterestRate { get; set; }
    public decimal? CreditLimit { get; set; }
    public DateTime? CutoffDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}