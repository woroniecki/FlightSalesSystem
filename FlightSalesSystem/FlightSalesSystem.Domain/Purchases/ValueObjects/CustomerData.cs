using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Purchases.ValueObjects;
public class CustomerData : ValueObject
{
    private CustomerData(string firstName, string lastName, DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly BirthDate { get; }

    public static CustomerData Create(string firstName, string lastName, DateOnly birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        return new CustomerData(firstName, lastName, birthDate);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
        yield return BirthDate;
    }
}
