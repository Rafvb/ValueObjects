using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class ChildId : IdValueObject
{
    public ChildId(long id)
        : base(id)
    {
    }

    private ChildId()
    {
    }

    public static ChildId Empty => new();
}