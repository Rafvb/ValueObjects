namespace ValueObjects.PrimitiveObsession;

public sealed class PrimitiveChild
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public DateOnly Birthdate { get; set; }

    public string NationalNumber { get; set; }

    // And lots more...
}