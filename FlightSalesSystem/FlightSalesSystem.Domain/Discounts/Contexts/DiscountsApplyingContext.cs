using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Flights;
using FlightSalesSystem.Domain.Purchases.ValueObjects;

namespace FlightSalesSystem.Domain.Discounts.Contexts;
public class DiscountsApplyingContext
{
    public required Flight Flight { get; init; }
    public required CustomerData Customer { get; init; }
    public required DateTime FlightDate { get; init; }
    public required IEnumerable<IDiscountCriteria> DiscountsCriteriaToApply { get; init; }
    public required Money Price { get; init; }
}
