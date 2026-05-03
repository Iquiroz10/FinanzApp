using FinanzApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Data;

public class FinanzAppDbContext : DbContext
{
    public FinanzAppDbContext(DbContextOptions<FinanzAppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Debt> Debts => Set<Debt>();
    public DbSet<Investment> Investments => Set<Investment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FinanzAppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}