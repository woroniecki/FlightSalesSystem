using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Enums;

namespace FlightSalesSystem.Domain.Discounts.Criteria;
public class BirthdayDiscount : BaseDiscountCriteria
{
    public override bool IsApplicable(DiscountsApplyingContext context)
    {
        if (context.Customer.BirthDate.Month == context.FlightDate.Month &&
            context.Customer.BirthDate.Day == context.FlightDate.Day)
        {
            return true;
        }

        return false;
    }

    public override Discount Type => Discount.Birthday;
}
