using ValueObjects.Common;

namespace ValueObjects.Records;

public sealed class Address : ValueObject
{
    public Address(string street, string zipCode)
    {
        if (string.IsNullOrEmpty(street))
        {
            throw new BusinessRuleException("Street should not be empty");
        }

        if (string.IsNullOrEmpty(zipCode))
        {
            throw new BusinessRuleException("ZipCode should not be empty");
        }

        Street = street;
        ZipCode = zipCode;
    }

    public string Street { get; }

    public string ZipCode { get; }

    protected override IEnumerable<object> GetEqualityMembers()
    {
        yield return Street;
        yield return ZipCode;
    }
}