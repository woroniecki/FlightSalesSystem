using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Flights.ValueObjects;
public sealed class FlightPrice : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
