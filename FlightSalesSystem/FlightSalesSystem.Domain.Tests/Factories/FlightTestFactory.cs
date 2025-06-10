using System.Collections.ObjectModel;
using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Flights;
using FlightSalesSystem.Domain.Flights.ValueObjects;

namespace FlightSalesSystem.Domain.Tests.Factories;
public static class FlightTestFactory
{
    public static Flight CreateFlight(
       DateTime? priceFrom = null,
       DateTime? priceTo = null,
       string flightCode = "KLM 12345 BCA")
    {
        var flightId = FlightId.Create(flightCode);
        var from = new Airport();
        var to = new Airport();
        var departureTime = new TimeSpan(10, 30, 0);
        var daysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday };
        var daysOfWeekReadOnly = new ReadOnlyCollection<DayOfWeek>(daysOfWeek.ToList());
        var tenantId = Guid.NewGuid();

        var prices = new List<FlightPrice>();

        if (priceFrom.HasValue && priceTo.HasValue)
        {
            var dateRange = DateRange.Create(priceFrom.Value, priceTo.Value);
            prices.Add(FlightPrice.Create(Money.CreateEUR(10), dateRange));
        }

        return Flight.Create(flightId, from, to, departureTime, daysOfWeekReadOnly, tenantId, prices);
    }
}

