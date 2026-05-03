using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Repositories;

public interface IInvestmentRepository
{
    Task<IEnumerable<Investment>> GetAllByUserAsync(Guid userId);
    Task<Investment?> GetByIdAsync(Guid id);
    Task<Investment> AddAsync(Investment investment);
    Task UpdateAsync(Investment investment);
    Task DeleteAsync(Investment investment);
}