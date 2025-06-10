using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Discounts.Contexts;
using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Discounts.Enums;
using FlightSalesSystem.Domain.Discounts.Exceptions;
using FlightSalesSystem.Domain.Discounts.Services;
using FlightSalesSystem.Domain.Flights.Enums;
using FlightSalesSystem.Domain.Flights.ValueObjects;
using FlightSalesSystem.Domain.Purchases.ValueObjects;
using FlightSalesSystem.Domain.Tests.Factories;
using FluentAssertions;

namespace FlightSalesSystem.Domain.Tests.DiscountsTests;
public class DiscountsApplierTests
{
    [Fact]
    public void ApplyDiscounts_WhenCustomerHasBirthdayOnFlightDate_AppliesBirthdayDiscount()
    {
        // Arrange
        var originalPrice = Money.CreateEUR(100);
        var customer = CustomerData.Create("firstName", "lastName", new DateOnly(1990, 6, 10));
        var flightDate = new DateTime(2025, 6, 10);

        var context = new DiscountsApplyingContext
        {
            Flight = null!,
            Customer = customer,
            FlightDate = flightDate,
            Price = originalPrice,
            DiscountsCriteriaToApply = new List<IDiscountCriteria> { new BirthdayDiscount() }
        };

        var applier = new DiscountsApplier();

        // Act
        var (price, appliedDiscounts) = applier.ApplyDiscounts(context);

        // Assert
        appliedDiscounts.Should().HaveCount(1);
        appliedDiscounts.Should().Contain(d => d.Type == Discount.Birthday);
        price.Amount.Should().Be(originalPrice.Amount - 5);
    }

    [Fact]
    public void ApplyDiscounts_WhenFlightIsToAfricaOnThursday_AppliesThursdayAfricaDiscount()
    {
        // Arrange
        var originalPrice = Money.CreateEUR(100);
        var customer = CustomerData.Create("firstName", "lastName", new DateOnly(1990, 6, 10));
        var flightDate = new DateTime(2025, 6, 12);
        var flight = FlightTestFactory.CreateFlight(
            to: Airport.Create("OR Tambo", "Johannesburg", "South Africa", Continent.Africa)
            );

        var context = new DiscountsApplyingContext
        {
            Flight = flight,
            Customer = customer,
            FlightDate = flightDate,
            Price = originalPrice,
            DiscountsCriteriaToApply = new List<IDiscountCriteria> { new ThursdayAfricaDiscount() }
        };

        var applier = new DiscountsApplier();

        // Act
        var (price, appliedDiscounts) = applier.ApplyDiscounts(context);

        // Assert
        appliedDiscounts.Should().HaveCount(1);
        appliedDiscounts.Should().Contain(d => d.Type == Discount.ThursdayAfrica);
        price.Amount.Should().Be(originalPrice.Amount - 5);
    }

    [Fact]
    public void ApplyDiscounts_WhenTotalDiscountedPriceFallsBelowMinimum20_ThrowsException()
    {
        // Arrange
        var originalPrice = Money.CreateEUR(25);
        var customer = CustomerData.Create("firstName", "lastName", new DateOnly(1990, 6, 12));
        var flightDate = new DateTime(2025, 6, 12);
        var flight = FlightTestFactory.CreateFlight(
            to: Airport.Create("OR Tambo", "Johannesburg", "South Africa", Continent.Africa)
            );

        var context = new DiscountsApplyingContext
        {
            Flight = flight,
            Customer = customer,
            FlightDate = flightDate,
            Price = originalPrice,
            DiscountsCriteriaToApply = new List<IDiscountCriteria> { new ThursdayAfricaDiscount(), new BirthdayDiscount() }
        };

        var applier = new DiscountsApplier();

        // Act
        Action act = () => applier.ApplyDiscounts(context);

        // Assert
        act.Should().Throw<DiscountedPriceTooLowException>();
    }
}
