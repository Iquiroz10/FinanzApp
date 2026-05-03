using FinanzApp.Application.DTOs.Debt;

namespace FinanzApp.Application.Interfaces.Services;

public interface IDebtService
{
    Task<IEnumerable<DebtResponseDto>> GetAllByUserAsync(Guid userId);
    Task<DebtResponseDto?> GetByIdAsync(Guid id);
    Task<DebtResponseDto> CreateAsync(Guid userId, DebtCreateDto dto);
    Task UpdateAsync(Guid id, DebtCreateDto dto);
    Task DeleteAsync(Guid id);
}