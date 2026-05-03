namespace FinanzApp.Application.DTOs.Investment;

public class InvestmentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal InvestedAmount { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal ReturnRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? MaturityDate { get; set; }
}