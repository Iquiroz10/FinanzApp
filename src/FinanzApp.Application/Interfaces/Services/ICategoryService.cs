using FinanzApp.Application.DTOs.Category;

namespace FinanzApp.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(Guid id);
    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
}