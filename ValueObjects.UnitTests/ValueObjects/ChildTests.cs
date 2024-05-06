using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class ChildTests
{
    [Fact]
    public void Create_CreatesValidChild()
    {
        var child = new Child(
            new ChildId(5),
            new CustomerId(456),
            new ChildName("Van Baelen", "Raf"),
            new Birthdate(new DateOnly(2021, 1, 1)),
            new NationalNumber("21010101346"));

        child.Id.Should().Be(new ChildId(5));
        child.CustomerId.Should().Be(new CustomerId(456));
        child.Name.Should().Be(new ChildName("Van Baelen", "Raf"));
        child.Birthdate.Should().Be(new Birthdate(new DateOnly(2021, 1, 1)));
        child.NationalNumber.Should().Be(new NationalNumber("21010101346"));
    }
    
    [Fact]
    public void Create_WithoutId_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithId(null).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("ChildId should not be empty");
    }
    
    [Fact]
    public void Create_ChildIdEmpty_CreatesChild()
    {
        var child = new ChildBuilder().WithId(ChildId.Empty).Build();

        child.Id.Should().Be(ChildId.Empty);
    }

    [Fact]
    public void Create_WithoutCustomerId_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithCustomerId(null).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("CustomerId should not be empty");
    }

    [Fact]
    public void Create_WithCustomerIdEmpty_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithCustomerId(CustomerId.Empty).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("CustomerId should not be empty");
    }

    [Fact]
    public void Create_WithoutChildName_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithName(null).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Name should not be empty");
    }

    [Fact]
    public void Create_WithoutBirthdate_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithBirthdate(null).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Birthdate should not be empty");
    }

    [Fact]
    public void Create_WithoutNationalNumber_ThrowsBusinessRuleException()
    {
        FluentActions.Invoking(() => new ChildBuilder().WithNationalNumber(null).Build())
            .Should().Throw<BusinessRuleException>()
            .WithMessage("National number should not be empty");
    }

    [Fact]
    public void Change_WithChildName_ChangesName()
    {
        var child = new ChildBuilder().Build();

        child.Change(new ChildName("New", "Name"), Birthdate.Unknown, NationalNumber.Empty);

        child.Name.Should().Be(new ChildName("New", "Name"));
    }

    [Fact]
    public void Change_WithoutChildName_ThrowsBusinessRuleException()
    {
        var child = new ChildBuilder().Build();

        FluentActions.Invoking(() => child.Change(null, Birthdate.Unknown, NationalNumber.Empty))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Name should not be empty");
    }

    [Fact]
    public void Change_WithBirthdate_ChangesBirthdate()
    {
        var child = new ChildBuilder()
            .WithNationalNumber(NationalNumber.Empty)
            .Build();

        child.Change(new ChildName("New", "Name"), new Birthdate(new DateOnly(2023, 1, 1)), NationalNumber.Empty);

        child.Birthdate.Should().Be(new Birthdate(new DateOnly(2023, 1, 1)));
    }

    [Fact]
    public void Change_WithoutBirthdate_ThrowsBusinessRuleException()
    {
        var child = new ChildBuilder().Build();

        FluentActions.Invoking(() => child.Change(new ChildName("New", "Name"), null, NationalNumber.Empty))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Birthdate should not be empty");
    }

    [Fact]
    public void Change_WithNationalNumber_ChangesNationalNumber()
    {
        var child = new ChildBuilder()
            .WithBirthdate(new Birthdate(new DateOnly(2022, 7, 15)))
            .WithNationalNumber(NationalNumber.Empty)
            .Build();

        child.Change(new ChildName("New", "Name"), new Birthdate(new DateOnly(2022, 7, 15)), new NationalNumber("22071501378"));

        child.NationalNumber.Should().Be(new NationalNumber("22071501378"));
    }

    [Fact]
    public void Change_WithoutNationalNumber_ThrowsBusinessRuleException()
    {
        var child = new ChildBuilder().Build();

        FluentActions.Invoking(() => child.Change(new ChildName("New", "Name"), new Birthdate(new DateOnly(2023, 1, 1)), null))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("National number should not be empty");
    }

    [Fact]
    public void Change_WithMismatchBetweenBirthdateAndNationalNumber_ThrowsBusinessRuleException()
    {
        var child = new ChildBuilder().Build();

        FluentActions.Invoking(() => child.Change(new ChildName("New", "Name"), new Birthdate(new DateOnly(2023, 1, 1)), new NationalNumber("22071501378")))
            .Should().Throw<BusinessRuleException>()
            .WithMessage("Birthdate 01/01/2023 does not match birthdate in National number 22.07.15-013.78");
    }

    [Fact]
    public void Change_WithBirthdateUnknownAndNationalNumber_DoesNotValidateNationalNumber()
    {
        var child = new ChildBuilder().Build();
        
        child.Change(new ChildName("New", "Name"), Birthdate.Unknown, new NationalNumber("22071501378"));

        child.Birthdate.Should().Be(Birthdate.Unknown);
        child.NationalNumber.Should().Be(new NationalNumber("22071501378"));
    }

    [Fact]
    public void Change_WithBirthdateKnownAndNationalNumberEmpty_DoesNotValidateNationalNumber()
    {
        var child = new ChildBuilder().Build();

        child.Change(new ChildName("New", "Name"), new Birthdate(new DateOnly(2023, 1, 1)), NationalNumber.Empty);

        child.Birthdate.Should().Be(new Birthdate(new DateOnly(2023, 1, 1)));
        child.NationalNumber.Should().Be(NationalNumber.Empty);
    }
}