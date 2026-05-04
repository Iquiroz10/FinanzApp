namespace FinanzApp.Application.DTOs.Dashboard;

public class ExpenseAnalysisDto
{
    public decimal TotalExpenses { get; set; }
    public List<ExpenseByCategoryDto> ByCategory { get; set; } = [];
    public List<ExpenseByCategoryDto> TopCategories { get; set; } = [];
    public List<RecurringExpenseDto> RecurringExpenses { get; set; } = [];
}

public class ExpenseByCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public decimal Percentage { get; set; }
    public int Count { get; set; }
}

public class RecurringExpenseDto
{
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}