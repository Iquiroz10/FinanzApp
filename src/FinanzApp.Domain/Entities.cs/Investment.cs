using FinanzApp.Domain.Common;

namespace FinanzApp.Domain.Entities;

public class Investment : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal InvestedAmount { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal ReturnRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? MaturityDate { get; set; }

    // Navegación
    public User User { get; set; } = null!;
}