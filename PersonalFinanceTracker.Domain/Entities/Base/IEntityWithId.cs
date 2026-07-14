namespace PersonalFinanceTracker.Domain.Entities.Base;

public interface IEntityWithId : IEntity
{
    public object? RawId { get; }
}