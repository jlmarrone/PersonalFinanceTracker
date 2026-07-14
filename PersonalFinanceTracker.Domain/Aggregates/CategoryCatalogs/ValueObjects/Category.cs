namespace PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;

public sealed record Category
{
    public string Value { get; }

    public Category(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Category name cannot be empty.", nameof(value));

        Value = value.Trim();
    }
}