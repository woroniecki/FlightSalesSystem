using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Common.Services;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Services;
using FlightSalesSystem.Domain.Purchases.Contexts;
using FlightSalesSystem.Domain.Purchases.Exceptions;

namespace FlightSalesSystem.Domain.Purchases.Services;
public class PurchaseService : IPurchaseService
{
    private readonly IDiscountsApplier _discountsApplier;
    private readonly IClock _clock;

    public PurchaseService(IDiscountsApplier discountsApplier, IClock clock)
    {
        _discountsApplier = discountsApplier;
        _clock = clock;
    }

    public Purchase PurchaseFlight(PurchaseContext context)
    {
        if (context.FlightDate < _clock.UtcNow)
            throw new FlightDateInPastException();

        var price = context.Flight.GetPrice(context.FlightDate);
        var (finalPrice, appliedDiscounts) = _discountsApplier.ApplyDiscounts(
            MapToDiscountsApplyingContext(context, price)
            );

        return Purchase.Create(
            flightId: context.Flight.Id,
            tenantId: context.Tenant.Id,
            absolutePrice: price,
            finalPrice: finalPrice,
        customerData: context.CustomerData,
            appliedDiscounts: appliedDiscounts.Select(d => d.Type).ToList()
        );
    }

    private DiscountsApplyingContext MapToDiscountsApplyingContext(PurchaseContext context, Money price)
    {
        return new DiscountsApplyingContext()
        {
            Flight = context.Flight,
            Customer = context.CustomerData,
            FlightDate = context.FlightDate,
            DiscountsCriteriaToApply = context.AvailableDiscounts,
            Price = price
        };
    }
}
