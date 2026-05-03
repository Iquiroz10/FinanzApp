using FinanzApp.Application.DTOs.Investment;

namespace FinanzApp.Application.Interfaces.Services;

public interface IInvestmentService
{
    Task<IEnumerable<InvestmentResponseDto>> GetAllByUserAsync(Guid userId);
    Task<InvestmentResponseDto?> GetByIdAsync(Guid id);
    Task<InvestmentResponseDto> CreateAsync(Guid userId, InvestmentCreateDto dto);
    Task UpdateAsync(Guid id, InvestmentCreateDto dto);
    Task DeleteAsync(Guid id);
}