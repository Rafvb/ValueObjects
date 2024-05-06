using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateRange(DateTime from, DateTime till)
    {
        if (from > till)
        {
            throw new BusinessRuleException("From should be same as or before Till");
        }

        From = from;
        Till = till;

        Duration = Till - From;
    }

    public DateTime From { get; }

    public DateTime Till { get; }

    public TimeSpan Duration { get; }

    public bool OverlapsWith(DateRange otherDateRange)
    {
        if (otherDateRange == null)
        {
            throw new BusinessRuleException("Other DateRange should not be null");
        }

        return From < otherDateRange.Till && otherDateRange.From < Till;
    }

    public bool Contains(DateTime date)
    {
        return From <= date && date <= Till;
    }

    public DateRange Extend(TimeSpan value)
    {
        return new DateRange(From, Till.Add(value));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return From;
        yield return Till;
    }
}