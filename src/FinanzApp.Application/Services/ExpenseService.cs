using AutoMapper;
using FinanzApp.Application.DTOs.Expense;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var expenses = await _repository.GetAllByUserAsync(userId);
        return _mapper.Map<IEnumerable<ExpenseResponseDto>>(expenses);
    }

    public async Task<ExpenseResponseDto?> GetByIdAsync(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id);
        return expense is null ? null : _mapper.Map<ExpenseResponseDto>(expense);
    }

    public async Task<ExpenseResponseDto> CreateAsync(Guid userId, ExpenseCreateDto dto)
    {
        var expense = _mapper.Map<Expense>(dto);
        expense.UserId = userId;

        var created = await _repository.AddAsync(expense);

        // Recargamos con Include para tener CategoryName en el response
        var full = await _repository.GetByIdAsync(created.Id);
        return _mapper.Map<ExpenseResponseDto>(full!);
    }

    public async Task UpdateAsync(Guid id, ExpenseCreateDto dto)
    {
        var expense = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Expense {id} not found");

        _mapper.Map(dto, expense);
        await _repository.UpdateAsync(expense);
    }

    public async Task DeleteAsync(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Expense {id} not found");

        await _repository.DeleteAsync(expense);
    }
}