using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Enums;
using FlightSalesSystem.Domain.Flights.Enums;

namespace FlightSalesSystem.Domain.Discounts.Criteria;
public class ThursdayAfricaDiscount : BaseDiscountCriteria
{
    public override bool IsApplicable(DiscountsApplyingContext context)
    {
        if (context.FlightDate.DayOfWeek != DayOfWeek.Thursday)
            return false;

        if (context.Flight.To.Continent != Continent.Africa)
            return false;

        return true;
    }

    public override Discount Type => Discount.ThursdayAfrica;
}
