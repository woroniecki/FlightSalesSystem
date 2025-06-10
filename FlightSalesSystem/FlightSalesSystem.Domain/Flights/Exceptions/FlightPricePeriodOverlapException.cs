using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Flights.Exceptions;
public class FlightPricePeriodOverlapException : DomainException
{
    public FlightPricePeriodOverlapException(DateTime from, DateTime to)
        : base($"Cannot add a price flight from {from:yyyy-MM-dd} to {to:yyyy-MM-dd} because it overlaps with an existing price period.")
    {
    }
}
