namespace PersonalFinanceTracker.Domain.Entities.Base;

public interface ITimeTrackable
{
    /// <summary>
    /// UTC Time/date when entity has been created
    /// </summary>
    DateTime CreatedAt { get; init; }
}