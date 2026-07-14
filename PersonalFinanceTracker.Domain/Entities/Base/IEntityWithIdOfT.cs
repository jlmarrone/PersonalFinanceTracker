namespace PersonalFinanceTracker.Domain.Entities.Base;

public interface IEntityWithId<out TKey> : IEntityWithId
{
    /// <summary>
    /// Identifier of the entity
    /// </summary>
    public TKey Id { get; }
}