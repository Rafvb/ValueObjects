using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class Birthdate : ValueObject
{
    public Birthdate(DateOnly date)
    {
        if (date.Year < 2000)
        {
            throw new BusinessRuleException("Birthdate year should be greater than 1999");
        }

        Date = date;
    }

    private Birthdate()
    {
        Date = DateOnly.MinValue;
    }

    public static Birthdate Unknown => new();

    public DateOnly Date { get; }

    public override string ToString()
    {
        return this == Unknown ? "Birthdate unknown" : Date.ToString("dd/MM/yyyy");
    }

    protected override IEnumerable<object> GetEqualityMembers()
    {
        yield return Date;
    }
}