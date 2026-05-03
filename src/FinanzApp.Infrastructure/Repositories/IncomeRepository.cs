using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Domain.Entities;
using FinanzApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly FinanzAppDbContext _context;

    public IncomeRepository(FinanzAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Income>> GetAllByUserAsync(Guid userId)
        => await _context.Incomes
            .Where(i => i.UserId == userId)
            .OrderByDescending(i => i.ReceivedDate)
            .ToListAsync();

    public async Task<Income?> GetByIdAsync(Guid id)
        => await _context.Incomes
            .FirstOrDefaultAsync(i => i.Id == id);

    public async Task<Income> AddAsync(Income income)
    {
        await _context.Incomes.AddAsync(income);
        await _context.SaveChangesAsync();
        return income;
    }

    public async Task UpdateAsync(Income income)
    {
        income.UpdatedAt = DateTime.UtcNow;
        _context.Incomes.Update(income);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Income income)
    {
        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();
    }
}