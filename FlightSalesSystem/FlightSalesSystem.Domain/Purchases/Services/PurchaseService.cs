using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Purchases.Contexts;

namespace FlightSalesSystem.Domain.Purchases.Services;
public class PurchaseService : IPurchaseService
{
    public Purchase PurchaseFlight(PurchaseContext context)
    {
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
