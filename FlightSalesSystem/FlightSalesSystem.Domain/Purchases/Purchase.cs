using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Purchases.ValueObjects;

namespace FlightSalesSystem.Domain.Purchases;
public class Purchase : AggregateRoot
{
    public Purchase(Guid id) : base(id)
    {
    }

    public Guid FlightId { get; private set; }
    public Guid TenantId { get; private set; }
    public Money AbsolutePrice { get; private set; }
    public Money FinalPrice { get; private set; }
    public CustomerData CustomerData { get; private set; }
}
