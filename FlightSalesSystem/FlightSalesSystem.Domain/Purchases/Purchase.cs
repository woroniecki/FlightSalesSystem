using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Purchases;
public class Purchase : AggregateRoot
{
    public Purchase(Guid id) : base(id)
    {
    }
}
