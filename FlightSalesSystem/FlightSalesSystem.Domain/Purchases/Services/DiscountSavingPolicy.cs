using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Discounts.Enums;
using FlightSalesSystem.Domain.Tenants;

namespace FlightSalesSystem.Domain.Purchases.Services;
public class DiscountSavingPolicy : IDiscountSavingPolicy
{
    public IEnumerable<Discount> GetDiscountsToSave(Tenant tenant, IEnumerable<IDiscountCriteria> discounts)
    {
        return tenant.Group.ShouldSaveDiscounts
            ? discounts.Select(d => d.Type).ToList()
            : Array.Empty<Discount>();
    }
}
