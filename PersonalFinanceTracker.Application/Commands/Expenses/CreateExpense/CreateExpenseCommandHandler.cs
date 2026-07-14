using MediatR;

namespace PersonalFinanceTracker.Application.Commands.Expenses.CreateExpense;

public sealed class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
{
    // Dependencies: ExpenseFactory. IExpenseRepository. ICategoryCatalogRepository. IUserContext
    public async Task<Guid> Handle(
        CreateExpenseCommand request,
        CancellationToken cancellationToken)
    {
        // Load Current UserId using IUserContext
        // Check if CategoryCatalog Exists for User and Create It if not -> use ICategoryCatalogRepository
        // Create new Expense Record using ExpenseFactory
        // IExpenseRepository.Add new expense from previous step
        // Save Changes for repository
        // Return new Expense Id

        throw new NotImplementedException();
    }
}