using FlightSalesSystem.Domain.Common.Services;
using FlightSalesSystem.Domain.Discounts.Criteria;
using FlightSalesSystem.Domain.Discounts.Services;
using FlightSalesSystem.Domain.Flights;
using FlightSalesSystem.Domain.Purchases.Contexts;
using FlightSalesSystem.Domain.Purchases.Exceptions;
using FlightSalesSystem.Domain.Purchases.Services;
using FlightSalesSystem.Domain.Purchases.ValueObjects;
using FlightSalesSystem.Domain.Tenants;
using FlightSalesSystem.Domain.Tenants.Enums;
using FlightSalesSystem.Domain.Tests.Factories;
using FluentAssertions;
using Moq;

namespace FlightSalesSystem.Domain.Tests.PurchaseTests;
public class PurchaseServiceTests
{
    private readonly Mock<IClock> _clockMock = new();

    private PurchaseService CreateService(DateTime clockNow)
    {
        _clockMock.Setup(c => c.UtcNow).Returns(clockNow);
        return new PurchaseService(
            new DiscountsApplier(),
            _clockMock.Object);
    }

    private PurchaseContext CreateContext(
        Flight flight,
        Tenant tenant,
        CustomerData customer,
        DateTime flightDate,
        IEnumerable<IDiscountCriteria>? discounts = null)
    {
        return new PurchaseContext
        {
            Flight = flight,
            Tenant = tenant,
            CustomerData = customer,
            FlightDate = flightDate,
            AvailableDiscounts = discounts ?? Enumerable.Empty<IDiscountCriteria>()
        };
    }

    [Fact]
    public void PurchaseFlight_WithoutDiscounts_ReturnsExpectedPurchase()
    {
        //Arrange
        var now = new DateTime(2025, 6, 20);
        var flightDate = now.AddDays(1);
        var price = 100;

        var flight = FlightTestFactory.CreateFlight(
            priceFrom: flightDate, priceTo: flightDate, priceAmount: price,
            departureTime: flightDate.TimeOfDay, daysOfWeek: [flightDate.DayOfWeek]);
        var tenant = Tenant.Create("TestTenant", TenantGroup.A);
        var customer = CustomerData.Create("John", "Doe", new DateOnly(1990, 1, 1));

        var service = CreateService(now);
        var ctx = CreateContext(flight, tenant, customer, flightDate);

        //Act
        var purchase = service.PurchaseFlight(ctx);

        //Assert
        purchase.Should().NotBeNull();
        purchase.FlightId.Should().Be(flight.Id);
        purchase.TenantId.Should().Be(tenant.Id);
        purchase.AbsolutePrice.Amount.Should().Be(price);
        purchase.FinalPrice.Amount.Should().Be(price);
        purchase.CustomerData.Should().Be(customer);
        purchase.AppliedDiscounts.Should().BeEmpty();
    }

    [Fact]
    public void PurchaseFlight_WithPastFlightDate_ThrowsFlightDateInPastException()
    {
        //Arrange
        var now = new DateTime(2025, 6, 20);
        var flightDate = now.AddDays(-1);
        var flight = FlightTestFactory.CreateFlight();

        var service = CreateService(now);
        var ctx = new PurchaseContext
        {
            Flight = flight,
            Tenant = Tenant.Create("Test", TenantGroup.A),
            CustomerData = CustomerData.Create("Test", "User", new DateOnly(1990, 1, 1)),
            FlightDate = flightDate,
            AvailableDiscounts = Enumerable.Empty<IDiscountCriteria>()
        };

        //Act
        Action act = () => service.PurchaseFlight(ctx);

        //Assert
        act.Should().Throw<FlightDateInPastException>();
    }
}
