using AutoMapper;
using FinanzApp.Application.DTOs.Income;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _repository;
    private readonly IMapper _mapper;

    public IncomeService(IIncomeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IncomeResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var incomes = await _repository.GetAllByUserAsync(userId);
        return _mapper.Map<IEnumerable<IncomeResponseDto>>(incomes);
    }

    public async Task<IncomeResponseDto?> GetByIdAsync(Guid id)
    {
        var income = await _repository.GetByIdAsync(id);
        return income is null ? null : _mapper.Map<IncomeResponseDto>(income);
    }

    public async Task<IncomeResponseDto> CreateAsync(Guid userId, IncomeCreateDto dto)
    {
        var income = _mapper.Map<Income>(dto);
        income.UserId = userId;

        var created = await _repository.AddAsync(income);
        return _mapper.Map<IncomeResponseDto>(created);
    }

    public async Task UpdateAsync(Guid id, IncomeCreateDto dto)
    {
        var income = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Income {id} not found");

        _mapper.Map(dto, income);
        await _repository.UpdateAsync(income);
    }

    public async Task DeleteAsync(Guid id)
    {
        var income = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Income {id} not found");

        await _repository.DeleteAsync(income);
    }
}