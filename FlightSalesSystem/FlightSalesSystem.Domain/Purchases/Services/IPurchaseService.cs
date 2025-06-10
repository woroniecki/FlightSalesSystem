using FlightSalesSystem.Domain.Purchases.Contexts;

namespace FlightSalesSystem.Domain.Purchases.Services;
public interface IPurchaseService
{
    Purchase PurchaseFlight(PurchaseContext context);
}
