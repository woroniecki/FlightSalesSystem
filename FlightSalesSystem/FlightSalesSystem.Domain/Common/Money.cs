using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Common;
public class Money : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
