using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Flights;
using FlightSalesSystem.Domain.Purchases.ValueObjects;
using FlightSalesSystem.Domain.Tenants;

namespace FlightSalesSystem.Domain.Purchases.Contexts;
public class PurchaseContext
{
    public required Flight Flight { get; init; }
    public required CustomerData CustomerData { get; init; }
    public required DateTime FlightDate { get; init; }
    public required Tenant Tenant { get; init; }
    public required IEnumerable<IDiscountCriteria> AvailableDiscounts { get; init; }
}
