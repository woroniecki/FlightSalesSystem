using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Discounts.Enums;
using FlightSalesSystem.Domain.Tenants;

namespace FlightSalesSystem.Domain.Purchases.Services;
public interface IDiscountSavingPolicy
{
    IEnumerable<Discount> GetDiscountsToSave(Tenant tenant, IEnumerable<IDiscountCriteria> discounts);
}
