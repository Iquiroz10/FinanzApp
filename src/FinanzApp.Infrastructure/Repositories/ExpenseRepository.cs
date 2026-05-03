using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Domain.Entities;
using FinanzApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly FinanzAppDbContext _context;

    public ExpenseRepository(FinanzAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetAllByUserAsync(Guid userId)
        => await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();

    public async Task<Expense?> GetByIdAsync(Guid id)
        => await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Expense>> GetByCategoryAsync(Guid userId, Guid categoryId)
        => await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.UserId == userId && e.CategoryId == categoryId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();

    public async Task<Expense> AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task UpdateAsync(Expense expense)
    {
        expense.UpdatedAt = DateTime.UtcNow;
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}