using System.Collections.ObjectModel;
using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Flights;
using FlightSalesSystem.Domain.Flights.Enums;
using FlightSalesSystem.Domain.Flights.ValueObjects;

namespace FlightSalesSystem.Domain.Tests.Factories;
public static class FlightTestFactory
{
    public static Flight CreateFlight(
       Airport? from = null,
       Airport? to = null,
       DateTime? priceFrom = null,
       DateTime? priceTo = null,
       decimal priceAmount = 1,
       string flightCode = "KLM 12345 BCA")
    {
        var flightId = FlightId.Create(flightCode);
        from = from ?? Airport.Create("Chopin", "Warsaw", "Poland", Continent.Europe);
        to = to ?? Airport.Create("Heathrow", "London", "United Kingdom", Continent.Europe);
        var departureTime = new TimeSpan(10, 30, 0);
        var daysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday };
        var daysOfWeekReadOnly = new ReadOnlyCollection<DayOfWeek>(daysOfWeek.ToList());
        var tenantId = Guid.NewGuid();

        var prices = new List<FlightPrice>();

        if (priceFrom.HasValue && priceTo.HasValue)
        {
            var dateRange = DateRange.Create(priceFrom.Value, priceTo.Value);
            prices.Add(FlightPrice.Create(Money.CreateEUR(priceAmount), dateRange));
        }

        return Flight.Create(flightId, from, to, departureTime, daysOfWeekReadOnly, tenantId, prices);
    }
}

