using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Purchases.Exceptions;
public class FlightDateInPastException : DomainException
{
    public FlightDateInPastException() : base("Cannot purchase flight for a past date.") { }
}
