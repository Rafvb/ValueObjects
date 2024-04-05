using FluentAssertions;
using ValueObjects.Records;

namespace ValueObjects.UnitTests.Records;

public sealed class AddressRecordWithCommentsTests
{
    [Fact]
    public void CollectionComparison_DoesNotWorkWithRecordTypes()
    {
        var address1 = new AddressRecordWithComments("Bekaflaan", "3200", ["Comment1", "Comment2"]);
        var address2 = new AddressRecordWithComments("Bekaflaan", "3200", ["Comment1", "Comment2"]);

        (address1 == address2).Should().BeTrue();
    }
}