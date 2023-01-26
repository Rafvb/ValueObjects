using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class BirthdateTests
{
    [Fact]
    public void Create_CreatesValidBirthdate()
    {
        var birthdate = new Birthdate(new DateOnly(2023, 10, 2));

        birthdate.Date.Should().Be(new DateOnly(2023, 10, 2));
    }
    
    [Theory]
    [InlineData(1999)]
    [InlineData(1)]
    public void Create_YearBefore2000_ThrowsBusinessRuleException(int year)
    {
        FluentActions.Invoking(() => new Birthdate(new DateOnly(year, 10, 2)))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Birthdate year should be greater than 1999");
    }

    [Fact]
    public void Create_Year2000_CreatesValidBirthdate()
    {
        var birthdate = new Birthdate(new DateOnly(2000, 10, 2));

        birthdate.Date.Should().Be(new DateOnly(2000, 10, 2));
    }

    [Fact]
    public void Unknown_CreatesUnknownBirthdate()
    {
        var unknownBirthdate = Birthdate.Unknown;

        unknownBirthdate.Date.Should().Be(DateOnly.MinValue);

        unknownBirthdate.Equals(Birthdate.Unknown).Should().BeTrue();
        unknownBirthdate.Equals(new Birthdate(new DateOnly(2000, 10, 2))).Should().BeFalse();
    }
    
    [Fact]
    public void ToString_ReturnsFormattedBirthdate()
    {
        var birthdate = new Birthdate(new DateOnly(2000, 10, 2));
        
        birthdate.ToString().Should().Be("02/10/2000");
    }

    [Fact]
    public void ToString_Unkown_ReturnsBirthdateUnknownText()
    {
        var unknownBirthdate = Birthdate.Unknown;

        unknownBirthdate.ToString().Should().Be("Birthdate unknown");
    }
}