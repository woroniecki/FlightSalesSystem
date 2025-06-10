using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Flights.ValueObjects;

namespace FlightSalesSystem.Domain.Flights;
public interface IFlightRepository : IRepository<Flight>
{
    Task<Flight?> GetFlightByFlightIdAsync(FlightId flightId);
}
