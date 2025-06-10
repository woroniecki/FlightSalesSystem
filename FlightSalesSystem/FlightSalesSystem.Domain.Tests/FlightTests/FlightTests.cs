using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Flights.Exceptions;
using FlightSalesSystem.Domain.Tests.Factories;
using FluentAssertions;

namespace FlightSalesSystem.Domain.Tests.FlightTests;
public class FlightTests
{
    [Fact]
    public void AddPriceForPeriod_WithNonOverlappingPeriod_ShouldAddPrice()
    {
        // Arrange
        var flight = FlightTestFactory.CreateFlight();
        var newFrom = new DateTime(2025, 01, 01);
        var newTo = newFrom.AddDays(5);
        var newPrice = Money.CreateEUR(10);

        // Act
        flight.AddPriceForPeriod(newFrom, newTo, newPrice);

        // Assert
        flight.FlightPrices.Should().HaveCount(1);
        flight.FlightPrices.First().Price.Should().Be(newPrice);
    }

    [Fact]
    public void AddPriceForPeriod_WithOverlappingPeriod_ShouldThrowFlightPricePeriodOverlapException()
    {
        // Arrange
        var priceFrom = new DateTime(2025, 01, 01);
        var priceTo = priceFrom.AddDays(5);
        var flight = FlightTestFactory.CreateFlight(priceFrom: priceFrom, priceTo: priceTo);

        var overlappingFrom = priceFrom.AddDays(3);
        var overlappingTo = priceFrom.AddDays(10);
        var overlappingPrice = Money.CreateEUR(10);

        // Act
        Action act = () => flight.AddPriceForPeriod(overlappingFrom, overlappingTo, overlappingPrice);

        // Assert
        act.Should().Throw<FlightPricePeriodOverlapException>();
    }
}
