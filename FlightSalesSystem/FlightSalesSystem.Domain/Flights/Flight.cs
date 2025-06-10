using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Flights.ValueObjects;

namespace FlightSalesSystem.Domain.Flights;
public class Flight : AggregateRoot
{
    private Flight(Guid id) : base(id)
    {
    }

    public FlightId FlightId { get; private set; }
    public Airport From { get; private set; }
    public Airport To { get; private set; }
    public TimeSpan DepartureTime { get; private set; }
    public IReadOnlyCollection<DayOfWeek> DaysOfWeek { get; private set; }
    public Guid TenantId { get; private set; }
    public IReadOnlyCollection<FlightPrice> FlightPrices { get; private set; }
}
