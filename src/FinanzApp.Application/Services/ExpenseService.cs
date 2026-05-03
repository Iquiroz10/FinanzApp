using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repository;   

    public ExpenseService(IExpenseRepository repository)
    {
        _repository = repository;        
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var expenses = await _repository.GetAllByUserAsync(userId);
        return expenses.Adapt<IEnumerable<ExpenseResponseDto>>();
    }

    public async Task<ExpenseResponseDto?> GetByIdAsync(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id);
        return expense?.Adapt<ExpenseResponseDto>();
    }

    public async Task<ExpenseResponseDto> CreateAsync(Guid userId, ExpenseCreateDto dto)
    {
        var expense = dto.Adapt<Expense>();
        expense.UserId = userId;

        var created = await _repository.AddAsync(expense);

        // Recargamos con Include para tener CategoryName en el response
        var full = await _repository.GetByIdAsync(created.Id);
        return full!.Adapt<ExpenseResponseDto>();
    }

    public async Task UpdateAsync(Guid id, ExpenseCreateDto dto)
    {
        var expense = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Expense {id} not found");

        dto.Adapt(expense);
        await _repository.UpdateAsync(expense);
    }

    public async Task DeleteAsync(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Expense {id} not found");

        await _repository.DeleteAsync(expense);
    }
}