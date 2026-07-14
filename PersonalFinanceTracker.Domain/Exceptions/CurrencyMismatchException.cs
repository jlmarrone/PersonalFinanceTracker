using PersonalFinanceTracker.Domain.Enums;

namespace PersonalFinanceTracker.Domain.Exceptions;

public sealed class CurrencyMismatchException : DomainException
{
    public Currency Left { get; }
    public Currency Right { get; }

    public CurrencyMismatchException(Currency left, Currency right)
        : base($"Cannot combine money of different currencies: {left} and {right}.")
    {
        Left = left;
        Right = right;
    }
}