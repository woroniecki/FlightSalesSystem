using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Purchases.Exceptions;
public class FlightNotAvailableException : DomainException
{
    public FlightNotAvailableException() : base("Flight is not available on the specified date.") { }
}
