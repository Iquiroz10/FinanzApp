using FinanzApp.Domain.Enums;

namespace FinanzApp.Application.DTOs.Category;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
}