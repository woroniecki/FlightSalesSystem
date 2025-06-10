using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Tenants;
public class Tenant : AggregateRoot
{
    public Tenant(Guid id) : base(id)
    {
    }
}
