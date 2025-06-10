using FlightSalesSystem.Domain.Common;
using FlightSalesSystem.Domain.Flights.Exceptions;
using FlightSalesSystem.Domain.Tests.Factories;
using FluentAssertions;

namespace FlightSalesSystem.Domain.Tests.FlightTests;
public class FlightTests
{
    [Fact]
    public void GetPrice_WithValidDate_ShouldReturnCorrectPrice()
    {
        // Arrange
        var priceFrom = new DateTime(2025, 01, 01);
        var priceTo = priceFrom.AddDays(5);
        var targetDate = priceFrom.AddDays(2);
        var expectedPrice = 1m;

        var flight = FlightTestFactory.CreateFlight(
            priceFrom: priceFrom,
            priceTo: priceTo,
            priceAmount: expectedPrice);

        // Act
        var result = flight.GetPrice(targetDate);

        // Assert
        result.Amount.Should().Be(expectedPrice);
    }

    [Fact]
    public void GetPrice_WhenNoPriceDefinedForDate_ShouldThrowFlightPriceNotDefinedException()
    {
        // Arrange
        var priceFrom = new DateTime(2025, 01, 01);
        var flight = FlightTestFactory.CreateFlight(
            priceFrom: priceFrom,
            priceTo: priceFrom.AddDays(1));

        var outOfRangeDate = priceFrom.AddDays(10);

        // Act
        Action act = () => flight.GetPrice(outOfRangeDate);

        // Assert
        act.Should().Throw<FlightPriceNotDefinedException>();
    }

    [Fact]
    public void AddPriceForPeriod_WithNonOverlappingPeriod_ShouldAddPrice()
    {
        // Arrange
        var flight = FlightTestFactory.CreateFlight();
        var newFrom = new DateTime(2025, 01, 01);
        var newTo = newFrom.AddDays(5);
        var newPrice = Money.CreateEUR(50);

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
        var flight = FlightTestFactory.CreateFlight(priceFrom: priceFrom, priceTo: priceTo, priceAmount: 100);

        var overlappingFrom = priceFrom.AddDays(3);
        var overlappingTo = priceFrom.AddDays(10);
        var overlappingPrice = Money.CreateEUR(200);

        // Act
        Action act = () => flight.AddPriceForPeriod(overlappingFrom, overlappingTo, overlappingPrice);

        // Assert
        act.Should().Throw<FlightPricePeriodOverlapException>();
    }

    [Fact]
    public void HasFlightOnDate_WhenDateMatches_ShouldReturnTrue()
    {
        // Arrange
        var flightDate = new DateTime(2025, 01, 01);
        var departureTime = new TimeSpan(10, 30, 0);
        var flight = FlightTestFactory.CreateFlight(
            departureTime: departureTime,
            daysOfWeek: new[] { flightDate.DayOfWeek });

        var dateToCheck = new DateTime(
            flightDate.Year,
            flightDate.Month,
            flightDate.Day,
            departureTime.Hours,
            departureTime.Minutes,
            0);

        // Act
        var result = flight.HasFlightOnDate(dateToCheck);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasFlightOnDate_WhenDateDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var departureTime = new TimeSpan(10, 30, 0);
        var flight = FlightTestFactory.CreateFlight(
            departureTime: departureTime,
            daysOfWeek: new[] { DayOfWeek.Monday });

        var dateToCheckNotMonday = new DateTime(2025, 1, 2, 10, 30, 0);

        // Act
        var result = flight.HasFlightOnDate(dateToCheckNotMonday);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasFlightOnDate_WhenTimeDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var flightDate = new DateTime(2025, 01, 01);
        var departureTime = new TimeSpan(10, 30, 0);
        var flight = FlightTestFactory.CreateFlight(
            departureTime: departureTime,
            daysOfWeek: new[] { flightDate.DayOfWeek });

        var dateToCheck = new DateTime(
            flightDate.Year,
            flightDate.Month,
            flightDate.Day,
            departureTime.Hours,
            departureTime.Minutes + 1,
            0);

        // Act
        var result = flight.HasFlightOnDate(dateToCheck);

        // Assert
        result.Should().BeFalse();
    }
}
