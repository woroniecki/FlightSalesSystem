using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Enums;
using FlightSalesSystem.Domain.Purchases.ValueObjects;

namespace FlightSalesSystem.Domain.Purchases;
public class Purchase : AggregateRoot
{
    public Purchase(Guid id,
        Guid flightId,
        Guid tenantId,
        Money absolutePrice,
        Money finalPrice,
        CustomerData customerData,
        IReadOnlyCollection<Discount> appliedDiscounts) : base(id)
    {
        FlightId = flightId;
        TenantId = tenantId;
        AbsolutePrice = absolutePrice;
        FinalPrice = finalPrice;
        CustomerData = customerData;
        AppliedDiscounts = appliedDiscounts;
    }

    public Guid FlightId { get; private set; }
    public Guid TenantId { get; private set; }
    public Money AbsolutePrice { get; private set; }
    public Money FinalPrice { get; private set; }
    public CustomerData CustomerData { get; private set; }
    public IReadOnlyCollection<Discount> AppliedDiscounts { get; private set; }

    public static Purchase Create(
       Guid flightId,
       Guid tenantId,
       Money absolutePrice,
       Money finalPrice,
       CustomerData customerData,
       IEnumerable<Discount> appliedDiscounts)
    {
        if (customerData == null) throw new ArgumentNullException(nameof(customerData));

        return new Purchase(
            id: Guid.NewGuid(),
            flightId: flightId,
            tenantId: tenantId,
            absolutePrice: absolutePrice,
            finalPrice: finalPrice,
            customerData: customerData,
            appliedDiscounts: appliedDiscounts.ToList().AsReadOnly());
    }
}
