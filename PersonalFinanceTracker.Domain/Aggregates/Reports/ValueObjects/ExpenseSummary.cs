using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;
using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.Domain.Aggregates.Reports.ValueObjects;

public sealed record ExpenseSummary(
    Guid ExpenseId,
    Money Amount,
    Category Category,
    DateTime ExpenseDate);