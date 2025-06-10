using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Discounts.Exceptions;

namespace FlightSalesSystem.Domain.Discounts.Services;
public class DiscountsApplier : IDiscountsApplier
{
    private static readonly Money MinimalDiscountedPrice = Money.CreateEUR(20m);
    public (Money price, IEnumerable<IDiscountCriteria> appliedDiscounts) ApplyDiscounts(
        DiscountsApplyingContext context)
    {
        List<IDiscountCriteria> appliedDiscounts = new List<IDiscountCriteria>();

        var finalPrice = context.Price;

        foreach (var discount in context.DiscountsCriteriaToApply.Distinct())
        {
            if (discount.IsApplicable(context))
            {
                finalPrice = discount.Apply(finalPrice);
                appliedDiscounts.Add(discount);
            }
        }

        ValidateDiscountPrice(finalPrice);

        return (finalPrice, appliedDiscounts);
    }

    private static void ValidateDiscountPrice(Money price)
    {
        if (price < MinimalDiscountedPrice)
            throw new DiscountedPriceTooLowException(MinimalDiscountedPrice);
    }
}

