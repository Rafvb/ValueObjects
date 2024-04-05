using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.Records;

namespace ValueObjects.UnitTests.Records;

public sealed class AddressTests
{
    [Fact]
    public void Create_CreatesAddress()
    {
        var address = new Address("Bekaflaan", "3200");

        address.Street.Should().Be("Bekaflaan");
        address.ZipCode.Should().Be("3200");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithoutStreet_ThrowsBusinessRuleException(string street)
    {
        FluentActions.Invoking(() => new Address(street, "3200"))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Street should not be empty");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithoutZipCode_ThrowsBusinessRuleException(string zipCode)
    {
        FluentActions.Invoking(() => new Address("Bekaflaan", zipCode))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("ZipCode should not be empty");
    }

    [Theory]
    [InlineData("Bekaflaan", "3200", true)]
    [InlineData("Stationstraat", "3200", false)]
    [InlineData("Bekaflaan", "3000", false)]
    public void Equality_ComparesByValue(string street, string zipCode, bool expectedEqual)
    {
        var address1 = new Address("Bekaflaan", "3200");
        var address2 = new Address(street, zipCode);

        address1.Equals(address2).Should().Be(expectedEqual);
        (address1 == address2).Should().Be(expectedEqual);
    }

    [Fact]
    public void Comparability_WorksWithValueObjects()
    {
        var address1 = new Address("Stationstraat", "3200");
        var address2 = new Address("Bekaflaan", "3200");
        var addresses = new[] { address1, address2 }.OrderBy(x => x).ToArray();

        addresses[0].Should().Be(address2);
    }

    [Fact]
    public void CollectionComparison_WorksWithValueObjects()
    {
        var address1 = new Address("Bekaflaan", "3200", ["Comment1", "Comment2"]);
        var address2 = new Address("Bekaflaan", "3200", ["Comment1", "Comment2"]);

        (address1 == address2).Should().BeTrue();
    }
}