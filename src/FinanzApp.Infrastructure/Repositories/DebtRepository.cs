using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Domain.Entities;
using FinanzApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Repositories;

public class DebtRepository : IDebtRepository
{
    private readonly FinanzAppDbContext _context;

    public DebtRepository(FinanzAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Debt>> GetAllByUserAsync(Guid userId)
        => await _context.Debts
            .Include(d => d.Category)
            .Where(d => d.UserId == userId)
            .OrderByDescending(d => d.RemainingBalance)
            .ToListAsync();

    public async Task<Debt?> GetByIdAsync(Guid id)
        => await _context.Debts
            .Include(d => d.Category)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Debt> AddAsync(Debt debt)
    {
        await _context.Debts.AddAsync(debt);
        await _context.SaveChangesAsync();
        return debt;
    }

    public async Task UpdateAsync(Debt debt)
    {
        debt.UpdatedAt = DateTime.UtcNow;
        _context.Debts.Update(debt);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Debt debt)
    {
        _context.Debts.Remove(debt);
        await _context.SaveChangesAsync();
    }
}