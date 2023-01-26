using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class NationalNumberTests
{
    [Fact]
    public void Create_CreatesValidNationalNumber()
    {
        var nationalNumber = new NationalNumber("21010101346");

        nationalNumber.Value.Should().Be("21010101346");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_NumberNullOrEmpty_ThrowsBusinessRuleException(string value)
    {
        FluentActions.Invoking(() => new NationalNumber(value))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Nationalnumber should not be empty");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2101010134")]
    [InlineData("210101013467")]
    public void Create_InvalidLength_ThrowsBusinessRuleException(string value)
    {
        FluentActions.Invoking(() => new NationalNumber(value))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Nationalnumber should have a length of 11");
    }

    [Theory]
    [InlineData("21011501346", 2021, 1, 15)]
    [InlineData("19120301346", 2019, 12, 3)]
    public void Create_CorrectlyParsesBirthdate(string value, int expectedYear, int expectedMonth, int expectedDay)
    {
        var nationalNumber = new NationalNumber(value);

        nationalNumber.Birthdate.Should().Be(new Birthdate(new DateOnly(expectedYear, expectedMonth, expectedDay)));
    }

    [Fact]
    public void Empty_CreatesEmptyNationalNumber()
    {
        var emptyNationalNumber = NationalNumber.Empty;

        emptyNationalNumber.Value.Should().Be(string.Empty);
        emptyNationalNumber.Birthdate.Should().Be(Birthdate.Unknown);

        emptyNationalNumber.Equals(NationalNumber.Empty).Should().BeTrue();
    }
    
    [Fact]
    public void ToString_ReturnsFormattedNationalNumber()
    {
        var nationalNumber = new NationalNumber("21010101346");
        
        nationalNumber.ToString().Should().Be("21.01.01-013.46");
    }

    [Fact]
    public void ToString_Empty_ReturnsNationalNumberEmptyText()
    {
        var emptyNationalNumber = NationalNumber.Empty;

        emptyNationalNumber.ToString().Should().Be("National number empty");
    }
}