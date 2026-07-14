namespace PersonalFinanceTracker.Domain.Entities.Base;

public abstract class Entity<TKey> : IEntityWithId<TKey>
{
    /// <summary>
    /// Identifier of the entity
    /// </summary>
    public TKey Id { get; init; }

    public object? RawId => Id;
}