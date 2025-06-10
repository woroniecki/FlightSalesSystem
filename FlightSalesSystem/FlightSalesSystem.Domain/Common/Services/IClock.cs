namespace FlightSalesSystem.Domain.Common.Services;
public interface IClock
{
    DateTime UtcNow { get; }
}
