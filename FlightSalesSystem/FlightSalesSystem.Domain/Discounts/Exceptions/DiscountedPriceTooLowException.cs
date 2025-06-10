using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Exceptions;

namespace FlightSalesSystem.Domain.Discounts.Exceptions;
public class DiscountedPriceTooLowException : DomainException
{
    public DiscountedPriceTooLowException(Money minimalPrice)
        : base($"Discounted price can't be below {minimalPrice}.") { }
}
