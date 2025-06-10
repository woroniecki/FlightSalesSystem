using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Flights.Exceptions;
public class InvalidFlightIdException : DomainException
{
    public InvalidFlightIdException(string message) : base(message) { }
}