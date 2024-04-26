using FluentAssertions;
using ValueObjects.Records;

namespace ValueObjects.UnitTests.Records;

public sealed class AddressRecordTests
{
    [Fact]
    public void Create_CreatesAddress()
    {
        var address = new AddressRecord("Bekaflaan", "3200");

        address.Street.Should().Be("Bekaflaan");
        address.ZipCode.Should().Be("3200");

        // Immutable:
        // address.Street = "Stationstraat";
    }

    [Theory]
    [InlineData("Bekaflaan", "3200", true)]
    [InlineData("Stationstraat", "3200", false)]
    [InlineData("Bekaflaan", "3000", false)]
    public void Equality_ComparesByValue(string street, string zipCode, bool expectedEqual)
    {
        var address1 = new AddressRecord("Bekaflaan", "3200");
        var address2 = new AddressRecord(street, zipCode);

        address1.Equals(address2).Should().Be(expectedEqual);
        (address1 == address2).Should().Be(expectedEqual);
    }

    [Fact]
    public void Comparability_DoesNotWorkWithRecordTypes()
    {
        var address1 = new AddressRecord("Stationstraat", "3200");
        var address2 = new AddressRecord("Bekaflaan", "3200");

        var addresses = new[] { address1, address2 }.OrderBy(x => x).ToArray();

        addresses[0].Should().Be(address2);
    }
}