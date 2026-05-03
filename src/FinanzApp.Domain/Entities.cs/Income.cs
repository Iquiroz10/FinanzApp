using FinanzApp.Domain.Common;
namespace FinanzApp.Domain.Entities;
public class Income : BaseEntity
{
  public Guid UserId { get; set; }
  public decimal Amount { get; set; }
  public DateTime ReceivedDate { get; set; }
  public DateTime PeriodStart { get; set; }
  public DateTime PeriodEnd { get; set; }
  public string? Notes { get; set; }

  // Navigation property
  public User User { get; set; } = null!; 
}
