using PersonalFinanceTracker.Domain.Exceptions;

namespace PersonalFinanceTracker.Domain.Aggregates.Reports.Exceptions;

public sealed class InvalidDateRangeException : DomainException
{
    public DateTime DateFrom { get; }
    public DateTime DateTo { get; }

    public InvalidDateRangeException(DateTime dateFrom, DateTime dateTo)
        : base($"Report date range is invalid: DateTo ({dateTo:yyyy-MM-dd}) must be on or after DateFrom ({dateFrom:yyyy-MM-dd}).")
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}