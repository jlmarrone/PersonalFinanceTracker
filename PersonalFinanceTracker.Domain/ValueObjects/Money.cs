using PersonalFinanceTracker.Domain.Enums;
using PersonalFinanceTracker.Domain.Exceptions;

namespace PersonalFinanceTracker.Domain.ValueObjects;

public sealed record Money
{
    public decimal Value { get; }
    public Currency Currency { get; }

    public Money(decimal value, Currency currency)
    {
        Value = value;
        Currency = currency;
    }

    public static Money Zero(Currency currency) => new(0, currency);

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new CurrencyMismatchException(left.Currency, right.Currency);
        }

        return new Money(left.Value + right.Value, left.Currency);
    }
}