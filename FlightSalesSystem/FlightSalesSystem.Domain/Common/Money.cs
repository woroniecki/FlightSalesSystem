using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Common.Enums;

namespace FlightSalesSystem.Domain.Common;
public sealed class Money : ValueObject
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    private static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be below 0.");

        return new Money(amount, currency);
    }

    public static Money CreateEUR(decimal amount) => Create(amount, Currency.EUR);

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);

        if (Amount < other.Amount)
            throw new InvalidOperationException("Cannot subtract a greater amount.");

        return new Money(Amount - other.Amount, Currency);
    }

    public bool GreaterThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount > other.Amount;
    }

    public bool LessThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount < other.Amount;
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Currency mismatch {Currency.Name}, {other.Currency.Name}.");
    }

    public static Money operator +(Money a, Money b) => a.Add(b);
    public static Money operator -(Money a, Money b) => a.Subtract(b);
    public static bool operator ==(Money a, Money b) => Equals(a, b);
    public static bool operator !=(Money a, Money b) => !Equals(a, b);
    public static bool operator >(Money a, Money b) => a.GreaterThan(b);
    public static bool operator <(Money a, Money b) => a.LessThan(b);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:0.00} {Currency}";
}
