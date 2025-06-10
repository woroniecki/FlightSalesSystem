using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Common;

namespace FlightSalesSystem.Domain.Flights.ValueObjects;
public sealed class FlightPrice : ValueObject
{
    private FlightPrice(Money price, DateRange validityPeriod)
    {
        Price = price;
        ValidityPeriod = validityPeriod;
    }

    public Money Price { get; }
    public DateRange ValidityPeriod { get; }

    public static FlightPrice Create(Money price, DateRange validityPeriod)
    {
        if (price is null)
            throw new ArgumentNullException(nameof(price), "Price cannot be null.");

        if (validityPeriod is null)
            throw new ArgumentNullException(nameof(validityPeriod), "Validity period cannot be null.");

        return new FlightPrice(price, validityPeriod);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Price;
        yield return ValidityPeriod;
    }
}
