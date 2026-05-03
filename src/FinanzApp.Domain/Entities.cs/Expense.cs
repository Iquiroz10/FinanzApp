
namespace FinanzApp.Domain.Entities;
public class Expense
{
  public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsRecurring { get; set; }

    // Navegación
    public User User { get; set; } = null!;
    public Category Category { get; set; } = null!;
  
}