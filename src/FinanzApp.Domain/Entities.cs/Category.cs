using FinanzApp.Domain.Common;
using FinanzApp.Domain.Enums;
namespace FinanzApp.Domain.Entities; 
public class Category : BaseEntity
{
  public string Name { get; set; } = string.Empty;
  public CategoryType Type { get; set; }

  // Navegación
  public ICollection<Income> Incomes { get; set; } = [];
  public ICollection<Expense> Expenses { get; set; } = [];
  
}