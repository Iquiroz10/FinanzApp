using FinanzApp.Application.DTOs.Category;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;        
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Adapt<IEnumerable<CategoryResponseDto>>();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category?.Adapt<CategoryResponseDto>();
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = dto.Adapt<Category>();
        var created = await _repository.AddAsync(category);
        return created.Adapt<CategoryResponseDto>();
    }
}