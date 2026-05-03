using AutoMapper;
using FinanzApp.Application.DTOs.Debt;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Services;

public class DebtService : IDebtService
{
    private readonly IDebtRepository _repository;
    private readonly IMapper _mapper;

    public DebtService(IDebtRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DebtResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var debts = await _repository.GetAllByUserAsync(userId);
        return _mapper.Map<IEnumerable<DebtResponseDto>>(debts);
    }

    public async Task<DebtResponseDto?> GetByIdAsync(Guid id)
    {
        var debt = await _repository.GetByIdAsync(id);
        return debt is null ? null : _mapper.Map<DebtResponseDto>(debt);
    }

    public async Task<DebtResponseDto> CreateAsync(Guid userId, DebtCreateDto dto)
    {
        var debt = _mapper.Map<Debt>(dto);
        debt.UserId = userId;

        var created = await _repository.AddAsync(debt);
        var full = await _repository.GetByIdAsync(created.Id);
        return _mapper.Map<DebtResponseDto>(full!);
    }

    public async Task UpdateAsync(Guid id, DebtCreateDto dto)
    {
        var debt = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Debt {id} not found");

        _mapper.Map(dto, debt);
        await _repository.UpdateAsync(debt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var debt = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Debt {id} not found");

        await _repository.DeleteAsync(debt);
    }
}