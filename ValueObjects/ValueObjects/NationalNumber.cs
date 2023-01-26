using System.Globalization;
using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class NationalNumber : ValueObject
{
    public NationalNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BusinessRuleException("Nationalnumber should not be empty");
        }

        // Strip out special characters like dots and dashes (for example 21.01.01-013.46)

        if (value.Length != 11)
        {
            throw new BusinessRuleException("Nationalnumber should have a length of 11");
        }

        // Validate the number using modulo 97 and the validation digits at the end

        Value = value;
        
        Birthdate = new Birthdate(DateOnly.ParseExact(value[..6], "yyMMdd", CultureInfo.CurrentCulture));

        // Parse out the gender
    }

    private NationalNumber()
    {
        Value = string.Empty;
        Birthdate = Birthdate.Unknown;
    }

    public static NationalNumber Empty => new();

    public string Value { get; }

    public Birthdate Birthdate { get; }
    
    public override string ToString()
    {
        return this == Empty ? "National number empty" : $"{Value[..2]}.{Value[2..4]}.{Value[4..6]}-{Value[6..9]}.{Value[9..11]}";
    }

    protected override IEnumerable<object> GetEqualityMembers()
    {
        yield return Value;
    }
}