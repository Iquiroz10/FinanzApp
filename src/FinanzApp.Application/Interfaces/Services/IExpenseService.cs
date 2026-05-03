using FinanzApp.Application.DTOs.Expense;

namespace FinanzApp.Application.Interfaces.Services;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseResponseDto>> GetAllByUserAsync(Guid userId);
    Task<ExpenseResponseDto?> GetByIdAsync(Guid id);
    Task<ExpenseResponseDto> CreateAsync(Guid userId, ExpenseCreateDto dto);
    Task UpdateAsync(Guid id, ExpenseCreateDto dto);
    Task DeleteAsync(Guid id);
}