using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Flights.Exceptions;
public class FlightPriceNotDefinedException : DomainException
{
    public FlightPriceNotDefinedException(DateTime date)
        : base($"No price defined for flight on {date:yyyy-MM-dd}.")
    {
    }
}
