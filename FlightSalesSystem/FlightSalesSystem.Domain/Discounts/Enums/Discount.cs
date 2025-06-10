using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Discounts.Enums;
public class Discount : Enumeration<Discount>
{
    public Discount(int value, string name) : base(value, name) { }

    public static readonly Discount Birthday = new Discount(1, nameof(Birthday));
    public static readonly Discount ThursdayAfrica = new Discount(2, nameof(ThursdayAfrica));
}
