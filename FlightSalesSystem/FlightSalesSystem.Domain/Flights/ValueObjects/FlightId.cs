using System.Text.RegularExpressions;
using FlightSalesSystem.Domain.Abstractions;
using FlightSalesSystem.Domain.Flights.Exceptions;

namespace FlightSalesSystem.Domain.Flights.ValueObjects;

public class FlightId : ValueObject
{
    private static readonly Regex FlightIdPattern = new Regex(
            @"^[A-Z]{3}\d{5}[A-Z]{3}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string IataCode { get; }
    public string NumericPart { get; }
    public string Suffix { get; }
    public string FullId => $"{IataCode}{NumericPart}{Suffix}";

    private FlightId(string iataCode, string numericPart, string suffix)
    {
        IataCode = iataCode;
        NumericPart = numericPart;
        Suffix = suffix;
    }

    public static FlightId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Invalid Flight ID format.", nameof(value));

        value = value.Replace(" ", "");

        if (value.Length != 11)
            throw new InvalidFlightIdException("Invalid Flight ID length.");

        if (!FlightIdPattern.IsMatch(value))
            throw new InvalidFlightIdException($"FlightId '{value}' is not in the correct format. Expected format: 3 letters, 5 digits, 3 letters (e.g. KLM12345BCA).");

        return new FlightId(
            iataCode: value.Substring(0, 3),
            numericPart: value.Substring(3, 5),
            suffix: value.Substring(8, 3));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return IataCode;
        yield return NumericPart;
        yield return Suffix;
    }
}
