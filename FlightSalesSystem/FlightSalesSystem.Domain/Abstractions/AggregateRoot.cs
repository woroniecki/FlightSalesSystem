namespace FlightSalesSystem.Domain.Abstractions;
public abstract class AggregateRoot : Entity
{
    public AggregateRoot(Guid id) : base(id)
    {
    }
}
