using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.Exceptions;
using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;
using PersonalFinanceTracker.Domain.Entities.Base;

namespace PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs;

public sealed class CategoryCatalog : TimeTrackableEntity<Guid>
{
    public Guid UserId { get; init; }

    private readonly List<Category> _categories = [];

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private CategoryCatalog(Guid userId)
    {
        UserId = userId;
        _categories = [];
    }

    public static CategoryCatalog Create(Guid userId) => new(userId);

    public void AddCategory(string categoryName)
    {
        var category = new Category(categoryName);

        if (_categories.Any(c => c.Value.Equals(category.Value, StringComparison.InvariantCultureIgnoreCase)))
        {
            throw new DuplicateCategoryException(category.Value);
        }

        _categories.Add(category);
    }
}