using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class ChildNr : NrValueObject
{
    public ChildNr(long nr)
        : base(nr)
    {
    }

    private ChildNr()
    {
    }

    public static ChildNr Empty => new();
}