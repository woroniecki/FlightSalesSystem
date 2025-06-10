using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Enums;

namespace FlightSalesSystem.Domain.Discounts.Criteria;
public abstract class BaseDiscountCriteria : IDiscountCriteria
{
    private static readonly Money DefaultDiscountValue = Money.CreateEUR(5m);

    public abstract bool IsApplicable(DiscountsApplyingContext context);

    public Money Apply(Money currentPrice)
    {
        return currentPrice.Subtract(DefaultDiscountValue);
    }

    public abstract Discount Type { get; }
}
