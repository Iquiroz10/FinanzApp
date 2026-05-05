using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _repository;
    public IncomeService(IIncomeRepository repository)
    {
        _repository = repository;        
    }

    public async Task<IEnumerable<IncomeResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var incomes = await _repository.GetAllByUserAsync(userId);
        return incomes.Adapt<IEnumerable<IncomeResponseDto>>();
    }

    public async Task<IncomeResponseDto?> GetByIdAsync(Guid id)
    {
        var income = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Income {id} not found");

        return income?.Adapt<IncomeResponseDto>();
    }

    public async Task<IncomeResponseDto> CreateAsync(Guid userId, IncomeCreateDto dto)
    {
        var income = dto.Adapt<Income>();
        income.UserId = userId;

        var created = await _repository.AddAsync(income);
        return created.Adapt<IncomeResponseDto>();
    }

    public async Task UpdateAsync(Guid id, IncomeCreateDto dto)
    {
        var income = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Income {id} not found");

        dto.Adapt(income);
        await _repository.UpdateAsync(income);
    }

    public async Task DeleteAsync(Guid id)
    {
        var income = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Income {id} not found");

        await _repository.DeleteAsync(income);
    }
}