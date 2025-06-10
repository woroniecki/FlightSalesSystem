using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Flights;
public class Flight : AggregateRoot
{
    public Flight(Guid id) : base(id)
    {
    }
}
