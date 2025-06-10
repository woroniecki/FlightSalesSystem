using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Criteria;

namespace FlightSalesSystem.Domain.Discounts.Services;
public interface IDiscountsApplier
{
    (Money price, IEnumerable<IDiscountCriteria> appliedDiscounts) ApplyDiscounts(DiscountsApplyingContext context);
}
