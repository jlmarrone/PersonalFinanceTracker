using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;
using PersonalFinanceTracker.Domain.Aggregates.Expenses;

namespace PersonalFinanceTracker.Infrastructure.Persistence.Configurations;

public sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.HasIndex(e => e.UserId);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.ExpenseDate)
            .IsRequired();

        builder.HasIndex(e => new { e.UserId, e.ExpenseDate });

        builder.OwnsOne(e => e.Amount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(e => e.Category)
            .HasConversion(
                category => category.Value,
                value => new Category(value))
            .HasColumnName("Category")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
