using PersonalFinanceTracker.Domain.Exceptions;

namespace PersonalFinanceTracker.Domain.Aggregates.Expenses.Exceptions;

public sealed class InvalidExpenseAmountException : DomainException
{
    public decimal Amount { get; }

    public InvalidExpenseAmountException(decimal amount)
        : base($"Expense amount must be greater than zero. Provided: {amount}.")
    {
        Amount = amount;
    }
}