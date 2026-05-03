using FinanzApp.Domain.Enums;

namespace FinanzApp.Application.DTOs.Category;

public class CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
}