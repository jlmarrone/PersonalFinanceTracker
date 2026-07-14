using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceTracker.Domain.Aggregates.Reports;
using PersonalFinanceTracker.Domain.Aggregates.Reports.ValueObjects;

namespace PersonalFinanceTracker.Infrastructure.Persistence.Configurations;

public sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.HasIndex(r => new { r.UserId, r.DateFrom });

        builder.Property(r => r.DateFrom)
            .IsRequired();

        builder.Property(r => r.DateTo)
            .IsRequired();

        builder.OwnsOne(r => r.TotalAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("TotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("TotalAmountCurrency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
        });

        var expenseSummariesComparer = new ValueComparer<IReadOnlyCollection<ExpenseSummary>>(
            (left, right) => (left ?? new List<ExpenseSummary>())
                .SequenceEqual(right ?? new List<ExpenseSummary>()),
            summaries => summaries
                .Aggregate(0, (hash, s) => HashCode.Combine(hash, s.GetHashCode())),
            summaries => summaries.ToList());

        builder.Property(r => r.ExpenseSummaries)
            .HasConversion(
                summaries => JsonSerializer.Serialize(summaries, JsonOptions),
                json => JsonSerializer.Deserialize<List<ExpenseSummary>>(json, JsonOptions)
                        ?? new List<ExpenseSummary>())
            .HasColumnName("ExpenseSummaries")
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(expenseSummariesComparer);

        builder.Property(r => r.CreatedAt)
            .IsRequired();
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
