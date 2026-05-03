using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Domain.Entities;
using FinanzApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Repositories;

public class InvestmentRepository : IInvestmentRepository
{
    private readonly FinanzAppDbContext _context;

    public InvestmentRepository(FinanzAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Investment>> GetAllByUserAsync(Guid userId)
        => await _context.Investments
            .Where(i => i.UserId == userId)
            .OrderByDescending(i => i.StartDate)
            .ToListAsync();

    public async Task<Investment?> GetByIdAsync(Guid id)
        => await _context.Investments
            .FirstOrDefaultAsync(i => i.Id == id);

    public async Task<Investment> AddAsync(Investment investment)
    {
        await _context.Investments.AddAsync(investment);
        await _context.SaveChangesAsync();
        return investment;
    }

    public async Task UpdateAsync(Investment investment)
    {
        investment.UpdatedAt = DateTime.UtcNow;
        _context.Investments.Update(investment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Investment investment)
    {
        _context.Investments.Remove(investment);
        await _context.SaveChangesAsync();
    }
}