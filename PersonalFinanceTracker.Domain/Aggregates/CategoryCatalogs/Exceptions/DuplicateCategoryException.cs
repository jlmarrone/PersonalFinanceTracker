using PersonalFinanceTracker.Domain.Exceptions;

namespace PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.Exceptions;

public sealed class DuplicateCategoryException : DomainException
{
    public string CategoryName { get; }

    public DuplicateCategoryException(string categoryName)
        : base($"A category named '{categoryName}' already exists.")
    {
        CategoryName = categoryName;
    }
}