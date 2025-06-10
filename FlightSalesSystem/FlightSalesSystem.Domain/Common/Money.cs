using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Common;
public sealed class Money : ValueObject
{
    public decimal Amount { get; init; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }
}
