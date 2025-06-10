using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Enums;

namespace FlightSalesSystem.Domain.Discounts.Criteria;
public interface IDiscountCriteria
{
    bool IsApplicable(DiscountsApplyingContext context);
    Money Apply(Money currentPrice);
    Discount Type { get; }
}
