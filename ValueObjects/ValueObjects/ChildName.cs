using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class ChildName : ValueObject
{
    public ChildName(string lastname, string firstname)
    {
        if (string.IsNullOrWhiteSpace(lastname))
        {
            throw new BusinessRuleException("Lastname should not be empty");
        }

        if (string.IsNullOrWhiteSpace(firstname))
        {
            throw new BusinessRuleException("Firstname should not be empty");
        }

        Lastname = lastname;
        Firstname = firstname;
    }

    public string Lastname { get; }

    public string Firstname { get; }

    public override string ToString()
    {
        return $"{Lastname} {Firstname}";
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Lastname;
        yield return Firstname;
    }
}