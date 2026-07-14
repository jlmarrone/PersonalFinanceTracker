namespace PersonalFinanceTracker.Domain.Entities.Base;

/// <summary>
/// Base type for any time-trackable entity
/// </summary>
/// <typeparam name="TKey">Primary key</typeparam>
public abstract class TimeTrackableEntity<TKey> : Entity<TKey>, ITimeTrackable
{
    /// <summary>
    /// Time/date when entity has been created
    /// </summary>
    public DateTime CreatedAt { get; init; }
}