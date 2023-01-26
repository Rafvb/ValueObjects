namespace ValueObjects.Common;

public abstract class NrValueObject : ValueObject
{
    protected NrValueObject()
    {
        Nr = 0;
        IsEmpty = true;
    }

    protected NrValueObject(long nr)
    {
        if (nr <= 0)
        {
            throw new BusinessRuleException($"{GetType().Name} must be greater than 0");
        }

        Nr = nr;
        IsEmpty = false;
    }

    public long Nr { get; }

    public bool IsEmpty { get; }

    public static implicit operator long(NrValueObject nrValueObject) => nrValueObject.Nr;

    public override string ToString()
    {
        return $"{Nr}";
    }

    protected override IEnumerable<object> GetEqualityMembers()
    {
        yield return Nr;
    }
}