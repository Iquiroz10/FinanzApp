using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Repositories;

public interface IDebtRepository
{
    Task<IEnumerable<Debt>> GetAllByUserAsync(Guid userId);
    Task<Debt?> GetByIdAsync(Guid id);
    Task<Debt> AddAsync(Debt debt);
    Task UpdateAsync(Debt debt);
    Task DeleteAsync(Debt debt);
}