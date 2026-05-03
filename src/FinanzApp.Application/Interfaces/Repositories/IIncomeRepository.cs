using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Repositories;

public interface IIncomeRepository
{
    Task<IEnumerable<Income>> GetAllByUserAsync(Guid userId);
    Task<Income?> GetByIdAsync(Guid id);
    Task<Income> AddAsync(Income income);
    Task UpdateAsync(Income income);
    Task DeleteAsync(Income income);
}