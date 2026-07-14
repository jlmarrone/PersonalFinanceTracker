using MediatR;

namespace PersonalFinanceTracker.Application.Commands.Expenses.CreateExpense;

public sealed record CreateExpenseCommand(
    string description,
    decimal amount,
    string currency,
    string category,
    DateTime expenseDate
) : IRequest<Guid>;