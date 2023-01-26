using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class CustomerNr : NrValueObject
{
    public CustomerNr(long nr)
        : base(nr)
    {
    }

    private CustomerNr()
    {
    }

    public static CustomerNr Empty => new();
}