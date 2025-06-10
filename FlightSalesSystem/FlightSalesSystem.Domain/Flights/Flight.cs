using System.Collections.ObjectModel;
using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Flights.Exceptions;
using FlightSalesSystem.Domain.Flights.ValueObjects;

namespace FlightSalesSystem.Domain.Flights;
public class Flight : AggregateRoot
{
    private Flight(
        Guid id,
        FlightId flightId,
        Airport from,
        Airport to,
        TimeSpan departureTime,
        IReadOnlyCollection<DayOfWeek> daysOfWeek,
        Guid tenantId) : base(id)
    {
        FlightId = flightId;
        From = from;
        To = to;
        DepartureTime = departureTime;
        DaysOfWeek = daysOfWeek;
        TenantId = tenantId;
        FlightPrices = new ReadOnlyCollection<FlightPrice>(new List<FlightPrice>());
    }

    public FlightId FlightId { get; private set; }
    public Airport From { get; private set; }
    public Airport To { get; private set; }
    public TimeSpan DepartureTime { get; private set; }
    public IReadOnlyCollection<DayOfWeek> DaysOfWeek { get; private set; }
    public Guid TenantId { get; private set; }
    public IReadOnlyCollection<FlightPrice> FlightPrices { get; private set; }

    public void AddPriceForPeriod(DateTime from, DateTime to, Money price)
    {
        var dateRange = DateRange.Create(from, to);

        if (FlightPrices.Any(p => p.ValidityPeriod.From <= to && p.ValidityPeriod.To >= from))
        {
            throw new FlightPricePeriodOverlapException(from, to);
        }

        var newPrices = FlightPrices.ToList();
        newPrices.Add(FlightPrice.Create(price, dateRange));
        FlightPrices = newPrices;
    }

    public Money GetPrice(DateTime date)
    {
        var price = FlightPrices
            .Where(p => p.ValidityPeriod.IsApplicableOn(date))
            .FirstOrDefault();

        if (price == null)
            throw new FlightPriceNotDefinedException(date);

        return price.Price;
    }

    public static Flight Create(
       FlightId flightId,
       Airport from,
       Airport to,
       TimeSpan departureTime,
       IReadOnlyCollection<DayOfWeek> daysOfWeek,
       Guid tenantId,
       IReadOnlyCollection<FlightPrice> prices)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (flightId == null) throw new ArgumentNullException(nameof(flightId));
        if (daysOfWeek == null || !daysOfWeek.Any()) throw new ArgumentException("Days of week cannot be null or empty", nameof(daysOfWeek));


        var flight = new Flight(
            id: Guid.NewGuid(),
            flightId: flightId,
            from: from,
            to: to,
            departureTime: departureTime,
            daysOfWeek: daysOfWeek,
            tenantId: tenantId
        );

        foreach (var flightPrice in prices)
        {
            flight.AddPriceForPeriod(
                flightPrice.ValidityPeriod.From,
                flightPrice.ValidityPeriod.To,
                flightPrice.Price);
        }

        return flight;
    }
}
