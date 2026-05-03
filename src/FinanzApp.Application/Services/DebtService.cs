using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Services;

public class DebtService : IDebtService
{
    private readonly IDebtRepository _repository;
    
    public DebtService(IDebtRepository repository)
    {
        _repository = repository;        
    }

    public async Task<IEnumerable<DebtResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var debts = await _repository.GetAllByUserAsync(userId);
        return debts.Adapt<IEnumerable<DebtResponseDto>>();
    }

    public async Task<DebtResponseDto?> GetByIdAsync(Guid id)
    {
        var debt = await _repository.GetByIdAsync(id);
        return debt?.Adapt<DebtResponseDto>();
    }

    public async Task<DebtResponseDto> CreateAsync(Guid userId, DebtCreateDto dto)
    {
        var debt = dto.Adapt<Debt>();
        debt.UserId = userId;

        var created = await _repository.AddAsync(debt);
        var full = await _repository.GetByIdAsync(created.Id);
        return full!.Adapt<DebtResponseDto>();
    }

    public async Task UpdateAsync(Guid id, DebtCreateDto dto)
    {
        var debt = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Debt {id} not found");

        dto.Adapt(debt);
        await _repository.UpdateAsync(debt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var debt = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Debt {id} not found");

        await _repository.DeleteAsync(debt);
    }
}