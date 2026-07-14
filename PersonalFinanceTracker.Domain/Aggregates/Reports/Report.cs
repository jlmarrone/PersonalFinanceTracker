using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;
using PersonalFinanceTracker.Domain.Aggregates.Reports.Exceptions;
using PersonalFinanceTracker.Domain.Aggregates.Reports.ValueObjects;
using PersonalFinanceTracker.Domain.Entities.Base;
using PersonalFinanceTracker.Domain.Enums;
using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.Domain.Aggregates.Reports;

public sealed class Report : TimeTrackableEntity<Guid>
{
    public Guid UserId { get; init; }

    public DateTime DateFrom { get; init; }

    public DateTime DateTo { get; init; }

    private readonly List<ExpenseSummary> _expenseSummaries;

    public IReadOnlyCollection<ExpenseSummary> ExpenseSummaries => _expenseSummaries;

    public Money TotalAmount { get; init; }

    private Report() {}

    private Report(
        Guid userId,
        DateTime dateFrom,
        DateTime dateTo,
        Money totalAmount,
        IReadOnlyCollection<ExpenseSummary> summaries)
    {
        UserId = userId;
        DateFrom = dateFrom;
        DateTo = dateTo;
        TotalAmount = totalAmount;
        _expenseSummaries = summaries.ToList();
    }

    public static Report Create(
        Guid userId,
        DateTime dateFrom,
        DateTime dateTo,
        Currency currency,
        IReadOnlyCollection<ExpenseSummary> summaries)
    {
        if (dateTo < dateFrom)
            throw new InvalidDateRangeException(dateFrom, dateTo);

        var totalAmount = summaries.Aggregate(
            Money.Zero(currency),
            (total, s) => total + s.Amount);

        return new Report(userId, dateFrom, dateTo, totalAmount, summaries);
    }
}