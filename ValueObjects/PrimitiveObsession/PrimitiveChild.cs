namespace ValueObjects.PrimitiveObsession;

public sealed class PrimitiveChild
{
    public long Nr { get; set; }

    public long CustomerNr { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public DateOnly Birthdate { get; set; }

    public string NationalNumber { get; set; }

    // And lots more...
}