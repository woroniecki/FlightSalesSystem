using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Flights.Enums;

namespace FlightSalesSystem.Domain.Flights.ValueObjects;
public sealed class Airport : ValueObject
{
    private Airport(string name, string city, string country, Continent continent)
    {
        Name = name;
        City = city;
        Country = country;
        Continent = continent;
    }

    public string Name { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public Continent Continent { get; init; }

    public static Airport Create(string name, string city, string country, Continent continent)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be null or empty.", nameof(country));

        if (continent is null)
            throw new ArgumentNullException(nameof(continent));

        return new Airport(name.Trim(), city.Trim(), country.Trim(), continent);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return City;
        yield return Country;
        yield return Continent;
    }
}
