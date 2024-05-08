using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class AmountTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(0.0)]
    [InlineData(0.01)]
    [InlineData(20.50)]
    [InlineData(-20.50)]
    public void Create_ValidInput_CreatesNewAmount(decimal quantity)
    {
        var amount = new Amount(quantity);

        amount.Quantity.Should().Be(quantity);
    }

    [Theory]
    [InlineData(0.001)]
    [InlineData(-0.001)]
    public void Create_InvalidInput_DoesNotCreateAmount(decimal quantity)
    {
        FluentActions.Invoking(() => new Amount(quantity))
            .Should()
            .Throw<BusinessRuleException>()
            .WithMessage("Amount can only contain 2 decimals");
    }

    [Fact]
    public void Operators_AllAretimaticOperatorsWork()
    {
        var amount1 = new Amount(10);
        var amount2 = new Amount(2);

        (amount1 + amount2).Should().Be(new Amount(12));

        (amount1 - amount2).Should().Be(new Amount(8));
        (amount2 - amount1).Should().Be(new Amount(-8));

        (amount1 * amount2).Should().Be(new Amount(20));

        (amount1 / amount2).Should().Be(new Amount(5));

        FluentActions.Invoking(() => amount1 / new Amount(0)).Should().Throw<DivideByZeroException>();
    }

    public static IEnumerable<object[]> ApplyPercentageData =>
    [
        [new Amount(100), 100, new Amount(100)],
        [new Amount(100), 60, new Amount(60)],
        [new Amount(100), 110, new Amount(110)],
        [new Amount(0), 50, new Amount(0)],
        [new Amount(29.51m), 60, new Amount(17.71m)],
        [new Amount(-100), 60, new Amount(-60m)],
        [new Amount(6.65m), 10, new Amount(0.67m)],
    ];

    [Theory]
    [MemberData(nameof(ApplyPercentageData))]
    public void ApplyPercentage_ReturnsNewAmountWithPercentageFromOrginal(
        Amount originalAmount,
        decimal percentage,
        Amount expectedAmount)
    {
        originalAmount.ApplyPercentage(percentage).Should().Be(expectedAmount);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    public void Negate_ReturnsNegatedQuantity(decimal quantity, decimal expectedQuantity)
    {
        new Amount(quantity).Negate().Should().Be(new Amount(expectedQuantity));
    }

    [Theory]
    [InlineData(0, "0,00")]
    [InlineData(1, "1,00")]
    [InlineData(-1, "-1,00")]
    [InlineData(0.0, "0,00")]
    [InlineData(0.01, "0,01")]
    [InlineData(20.50, "20,50")]
    [InlineData(-20.50, "-20,50")]
    [InlineData(120.50, "120,50")]
    [InlineData(-120.50, "-120,50")]
    public void ToString_ReturnsQuantityAsText(decimal quantity, string expectedText)
    {
        new Amount(quantity).ToString().Should().Be(expectedText);
    }
}