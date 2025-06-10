using FlightSalesSystem.Domain.Common.Services;
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
        return new PurchaseService(_clockMock.Object);
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
            FlightDate = flightDate
        };

        //Act
        Action act = () => service.PurchaseFlight(ctx);

        //Assert
        act.Should().Throw<FlightDateInPastException>();
    }
}
