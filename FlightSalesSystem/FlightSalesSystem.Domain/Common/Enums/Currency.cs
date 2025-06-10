using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Common.Enums;
public sealed class Currency : Enumeration<Currency>
{
    private Currency(int value, string name) : base(value, name) { }
    public static readonly Currency EUR = new(1, nameof(EUR));
}
