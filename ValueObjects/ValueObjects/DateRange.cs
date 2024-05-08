using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateRange(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new BusinessRuleException("From should be same as or before Till");
        }

        From = from;
        To = to;

        Duration = To - From;
    }

    public DateTime From { get; }

    public DateTime To { get; }

    public TimeSpan Duration { get; }

    public bool OverlapsWith(DateRange otherDateRange)
    {
        if (otherDateRange == null)
        {
            throw new BusinessRuleException("Other DateRange should not be null");
        }

        return From < otherDateRange.To && otherDateRange.From < To;
    }

    public bool Contains(DateTime date)
    {
        return From <= date && date <= To;
    }

    public DateRange Extend(TimeSpan value)
    {
        return new DateRange(From, To.Add(value));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return From;
        yield return To;
    }
}