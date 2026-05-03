using FinanzApp.Domain.Common;
using FinanzApp.Domain.Enums;
namespace FinanzApp.Domain.Entities;

public class Debt : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DebtType DebtType { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal RemainingBalance { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal InterestRate { get; set; }

    // Solo aplica para CreditCard
    public decimal? CreditLimit { get; set; }
    public DateTime? CutoffDate { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navegación
    public User User { get; set; } = null!;
    public Category Category { get; set; } = null!;
}