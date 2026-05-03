using FinanzApp.Domain.Common;
namespace FinanzApp.Domain.Entities;

public class User : BaseEntity
{
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty; 

  // Navegación
    public ICollection<Income> Incomes { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<Debt> Debts { get; set; } = [];
    public ICollection<Investment> Investments { get; set; } = []; 
  
}