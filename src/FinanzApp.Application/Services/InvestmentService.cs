
using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;
using Mapster;

namespace FinanzApp.Application.Services;

public class InvestmentService : IInvestmentService
{
    private readonly IInvestmentRepository _repository;
    

    public InvestmentService(IInvestmentRepository repository)
    {
        _repository = repository;        
    }

    public async Task<IEnumerable<InvestmentResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var investments = await _repository.GetAllByUserAsync(userId);
        return investments.Adapt<IEnumerable<InvestmentResponseDto>>();
    }

    public async Task<InvestmentResponseDto?> GetByIdAsync(Guid id)
    {
        var investment = await _repository.GetByIdAsync(id);
        return investment?.Adapt<InvestmentResponseDto>();
    }

    public async Task<InvestmentResponseDto> CreateAsync(Guid userId, InvestmentCreateDto dto)
    {
        var investment = dto.Adapt<Investment>();
        investment.UserId = userId;

        var created = await _repository.AddAsync(investment);      
        return created.Adapt<InvestmentResponseDto>();
    }

    public async Task UpdateAsync(Guid id, InvestmentCreateDto dto)
    {
        var investment = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Investment {id} not found");

        dto.Adapt(investment);
        await _repository.UpdateAsync(investment);
    }

    public async Task DeleteAsync(Guid id)
    {
        var investment = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Investment {id} not found");

        await _repository.DeleteAsync(investment);
    }
}