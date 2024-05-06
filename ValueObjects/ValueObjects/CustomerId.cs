using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class CustomerId : IdValueObject
{
    public CustomerId(long id)
        : base(id)
    {
    }

    private CustomerId()
    {
    }

    public static CustomerId Empty => new();
}