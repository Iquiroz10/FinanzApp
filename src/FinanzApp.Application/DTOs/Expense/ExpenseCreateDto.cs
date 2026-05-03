namespace FinanzApp.Application.DTOs.Expense;

public class ExpenseCreateDto
{
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsRecurring { get; set; }
}