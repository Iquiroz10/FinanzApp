using FinanzApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzApp.Infrastructure.Data.Configurations;

public class DebtConfiguration : IEntityTypeConfiguration<Debt>
{
    public void Configure(EntityTypeBuilder<Debt> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.DebtType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(d => d.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.RemainingBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.MonthlyPayment)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.InterestRate)
            .HasPrecision(5, 2);

        builder.Property(d => d.CreditLimit)
            .HasPrecision(18, 2);

        builder.HasOne(d => d.User)
            .WithMany(u => u.Debts)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Category)
            .WithMany(c => c.Debts)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}