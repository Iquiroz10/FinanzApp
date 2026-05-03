using FinanzApp.Domain.Enums;

namespace FinanzApp.Application.DTOs.Debt;

public class DebtResponseDto
{
    public Guid Id { get; set; }
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
    public string CategoryName { get; set; } = string.Empty;

    // Campo calculado — no viene de la BD, lo calcula el Service
    public int? RemainingPayments { get; set; }
}