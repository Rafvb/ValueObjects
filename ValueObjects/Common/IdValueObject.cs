namespace ValueObjects.Common;

public abstract class IdValueObject : ValueObject
{
    protected IdValueObject()
    {
        Id = 0;
        IsEmpty = true;
    }

    protected IdValueObject(long id)
    {
        if (id <= 0)
        {
            throw new BusinessRuleException($"{GetType().Name} must be greater than 0");
        }

        Id = id;
        IsEmpty = false;
    }

    public long Id { get; }

    public bool IsEmpty { get; }

    public static implicit operator long(IdValueObject idValueObject) => idValueObject.Id;

    public override string ToString()
    {
        return $"{Id}";
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
    }
}