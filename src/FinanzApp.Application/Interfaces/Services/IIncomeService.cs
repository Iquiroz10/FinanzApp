using FinanzApp.Application.DTOs.Income;

namespace FinanzApp.Application.Interfaces.Services;

public interface IIncomeService
{
    Task<IEnumerable<IncomeResponseDto>> GetAllByUserAsync(Guid userId);
    Task<IncomeResponseDto?> GetByIdAsync(Guid id);
    Task<IncomeResponseDto> CreateAsync(Guid userId, IncomeCreateDto dto);
    Task UpdateAsync(Guid id, IncomeCreateDto dto);
    Task DeleteAsync(Guid id);
}