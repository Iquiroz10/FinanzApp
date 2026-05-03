using AutoMapper;
using FinanzApp.Application.DTOs.Investment;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Services;

public class InvestmentService : IInvestmentService
{
    private readonly IInvestmentRepository _repository;
    private readonly IMapper _mapper;

    public InvestmentService(IInvestmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvestmentResponseDto>> GetAllByUserAsync(Guid userId)
    {
        var investments = await _repository.GetAllByUserAsync(userId);
        return _mapper.Map<IEnumerable<InvestmentResponseDto>>(investments);
    }

    public async Task<InvestmentResponseDto?> GetByIdAsync(Guid id)
    {
        var investment = await _repository.GetByIdAsync(id);
        return investment is null ? null : _mapper.Map<InvestmentResponseDto>(investment);
    }

    public async Task<InvestmentResponseDto> CreateAsync(Guid userId, InvestmentCreateDto dto)
    {
        var investment = _mapper.Map<Investment>(dto);
        investment.UserId = userId;

        var created = await _repository.AddAsync(investment);
        return _mapper.Map<InvestmentResponseDto>(created);
    }

    public async Task UpdateAsync(Guid id, InvestmentCreateDto dto)
    {
        var investment = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Investment {id} not found");

        _mapper.Map(dto, investment);
        await _repository.UpdateAsync(investment);
    }

    public async Task DeleteAsync(Guid id)
    {
        var investment = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Investment {id} not found");

        await _repository.DeleteAsync(investment);
    }
}