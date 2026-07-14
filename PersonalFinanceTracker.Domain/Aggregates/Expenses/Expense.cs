using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;
using PersonalFinanceTracker.Domain.Aggregates.Expenses.Exceptions;
using PersonalFinanceTracker.Domain.Entities.Base;
using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.Domain.Aggregates.Expenses;

public sealed class Expense : TimeTrackableEntity<Guid>
{
    public string Description { get; init; }

    public Money Amount { get; init; }

    public Guid UserId { get; init; }

    public Category Category { get; init; }

    public DateTime ExpenseDate { get; init; }

    private Expense() { }

    private Expense(
        string description,
        Money amount,
        Guid userId,
        Category category,
        DateTime expenseDate)
    {
        Description = description;
        Amount = amount;
        UserId = userId;
        Category = category;
        ExpenseDate = expenseDate;
    }

    public static Expense Create(string description, Money amount, Guid userId, Category category, DateTime expenseDate)
    {
        if (amount.Value <= 0)
        {
            throw new InvalidExpenseAmountException(amount.Value);
        }

        return new Expense(description, amount, userId, category, expenseDate);
    }
}