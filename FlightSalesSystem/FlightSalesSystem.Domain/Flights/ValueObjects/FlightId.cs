using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Flights.ValueObjects;
public sealed class FlightId : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
