using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Common.Services;
using FlightSalesSystem.Domain.Purchases.Contexts;
using FlightSalesSystem.Domain.Purchases.Exceptions;

namespace FlightSalesSystem.Domain.Purchases.Services;
public class PurchaseService : IPurchaseService
{
    private readonly IClock _clock;

    public PurchaseService(IClock clock)
    {
        _clock = clock;
    }

    public Purchase PurchaseFlight(PurchaseContext context)
    {
        if (context.FlightDate < _clock.UtcNow)
            throw new FlightDateInPastException();

        return Purchase.Create(
            flightId: context.Flight.Id,
            tenantId: context.Tenant.Id,
            absolutePrice: Money.CreateEUR(10),
            finalPrice: Money.CreateEUR(10),
            customerData: context.CustomerData,
            appliedDiscounts: null
        );
    }
}
