using FlightSalesSystem.Domain.Flights.Exceptions;
using FlightSalesSystem.Domain.Flights.ValueObjects;
using FluentAssertions;

namespace FlightSalesSystem.Domain.Tests.FlightTests;
public class FlightIdTests
{
    [Fact]
    public void Create_WithValidFlightId_ShouldReturnFlightIdObject()
    {
        // Arrange
        var validFlightId = "ABC12345XYZ";

        // Act
        var result = FlightId.Create(validFlightId);

        // Assert
        result.Should().NotBeNull();
        result.IataCode.Should().Be("ABC");
        result.NumericPart.Should().Be("12345");
        result.Suffix.Should().Be("XYZ");
        result.FullId.Should().Be("ABC12345XYZ");
    }

    [Theory]
    [InlineData("AB12345XYZ")]       // Too short (2 letters at the start)
    [InlineData("ABCD12345XYZ")]     // Too long (4 letters at the start)
    [InlineData("ABC1234XYZ")]       // Only 4 digits
    [InlineData("ABC123456XYZ")]     // 6 digits
    [InlineData("ABC12345XY")]       // Suffix too short
    [InlineData("ABC12345XYZZ")]     // Suffix too long
    [InlineData("12312345XYZ")]      // Starts with digits
    [InlineData("ABC12345X1Z")]      // Suffix has digits
    public void Create_WithInvalidFormat_ShouldThrowInvalidFlightIdException(string invalidId)
    {
        // Act
        Action act = () => FlightId.Create(invalidId);

        // Assert
        act.Should().Throw<InvalidFlightIdException>();
    }

    [Fact]
    public void Create_WithIncorrectLength_ShouldThrowInvalidFlightIdException()
    {
        // Arrange
        var invalidLengthId = "A1";

        // Act
        Action act = () => FlightId.Create(invalidLengthId);

        // Assert
        act.Should().Throw<InvalidFlightIdException>();
    }
}
