using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Flights.Enums;
public class Continent : Enumeration<Continent>
{
    private Continent(int id, string name) : base(id, name)
    {
    }

    public static readonly Continent Africa = new(1, nameof(Africa));
    public static readonly Continent Europe = new(2, nameof(Europe));
    public static readonly Continent Asia = new(3, nameof(Asia));
    public static readonly Continent NorthAmerica = new(4, nameof(NorthAmerica));
    public static readonly Continent SouthAmerica = new(5, nameof(SouthAmerica));
    public static readonly Continent Australia = new(6, nameof(Australia));
    public static readonly Continent Antarctica = new(7, nameof(Antarctica));
}
