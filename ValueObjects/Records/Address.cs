using ValueObjects.Common;

namespace ValueObjects.Records;

public sealed class Address : ValueObject
{
    public Address(string street, string zipCode) : this(street, zipCode, [])
    {
    }

    public Address(string street, string zipCode, string[] comments)
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

        Comments = comments ?? throw new BusinessRuleException("Comments should not be null");
    }

    public string Street { get; }

    public string ZipCode { get; }

    public string[] Comments { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Street;
        yield return ZipCode;

        foreach (var comment in Comments.OrderBy(c => c))
        {
            yield return comment;
        }
    }
}