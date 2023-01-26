using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class ChildNameTests
{
    [Fact]
    public void Create_CreatesChildName()
    {
        var childName = new ChildName("Last name", "First name");

        childName.Lastname.Should().Be("Last name");
        childName.Firstname.Should().Be("First name");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithoutLastname_ThrowsBusinessRuleException(string lastname)
    {
        FluentActions.Invoking(() => new ChildName(lastname, "First name"))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Lastname should not be empty");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithoutFirstname_ThrowsBusinessRuleException(string firstname)
    {
        FluentActions.Invoking(() => new ChildName("Last name", firstname))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Firstname should not be empty");
    }

    [Theory]
    [InlineData("Last name", "First name", true)]
    [InlineData("Laaaast name", "First name", false)]
    [InlineData("Last name", "Fiiiirst name", false)]
    public void Equality_ComparesByValue(string lastname, string firstname, bool expectedEqual)
    {
        var childName1 = new ChildName("Last name", "First name");
        var childName2 = new ChildName(lastname, firstname);

        childName1.Equals(childName2).Should().Be(expectedEqual);
        (childName1 == childName2).Should().Be(expectedEqual);
    }

    [Fact]
    public void ToString_ReturnsFormattedChildName()
    {
        var childName = new ChildName("Last name", "First name");
        
        childName.ToString().Should().Be("Last name First name");
    }
}