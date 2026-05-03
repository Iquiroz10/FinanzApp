using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Repositories;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetAllByUserAsync(Guid userId);
    Task<Expense?> GetByIdAsync(Guid id);
    Task<IEnumerable<Expense>> GetByCategoryAsync(Guid userId, Guid categoryId);
    Task<Expense> AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Expense expense);
}