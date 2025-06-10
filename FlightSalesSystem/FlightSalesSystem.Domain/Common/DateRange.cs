using FlightSalesSystem.Domain.Abstractions;

namespace FlightSalesSystem.Domain.Common;
public class DateRange : ValueObject
{
    private DateRange(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public DateTime From { get; }
    public DateTime To { get; }

    public static DateRange Create(DateTime from, DateTime to)
    {
        if (from > to)
            throw new ArgumentException("From date cannot be after To date.");

        return new DateRange(from, to);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return From;
        yield return To;
    }

    public bool IsApplicableOn(DateTime date) => date >= From && date <= To;
}
