namespace FlightSalesSystem.Domain.Abstractions;
public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
{
    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public int Value { get; }
    public string Name { get; } = string.Empty;

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
