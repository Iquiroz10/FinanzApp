using FinanzApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzApp.Infrastructure.Data.Configurations;

public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Institution)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.InvestedAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(i => i.CurrentValue)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(i => i.ReturnRate)
            .HasPrecision(5, 2);

        builder.HasOne(i => i.User)
            .WithMany(u => u.Investments)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}