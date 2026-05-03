namespace FinanzApp.Application.DTOs.Income;

public class IncomeCreateDto
{
    public decimal Amount { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string? Notes { get; set; }
}