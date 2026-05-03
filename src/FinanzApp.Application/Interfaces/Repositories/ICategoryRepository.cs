using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category> AddAsync(Category category);
}