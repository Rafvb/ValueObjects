using System.Globalization;
using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class Amount : ValueObject
{
    public Amount(decimal quantity)
    {
        if (quantity % 0.01m != 0)
        {
            throw new BusinessRuleException("Amount can only contain 2 decimals");
        }

        Quantity = quantity;
    }

    public decimal Quantity { get; }

    public static Amount operator +(Amount a, Amount b)
    {
        return new Amount(a.Quantity + b.Quantity);
    }

    public static Amount operator -(Amount a, Amount b)
    {
        return new Amount(a.Quantity - b.Quantity);
    }

    public static Amount operator *(Amount a, Amount b)
    {
        return new Amount(a.Quantity * b.Quantity);
    }

    public static Amount operator /(Amount a, Amount b)
    {
        if (b.Quantity == 0)
        {
            throw new DivideByZeroException();
        }

        return new Amount(a.Quantity / b.Quantity);
    }

    public static bool operator <(Amount a, Amount b)
    {
        return a.Quantity < b.Quantity;
    }

    public static bool operator >(Amount a, Amount b)
    {
        return a.Quantity > b.Quantity;
    }

    public static bool operator <=(Amount a, Amount b)
    {
        return a.Quantity <= b.Quantity;
    }

    public static bool operator >=(Amount a, Amount b)
    {
        return a.Quantity >= b.Quantity;
    }

    public Amount ApplyPercentage(decimal percentage)
    {
        return new Amount(Math.Round(Quantity * percentage / 100, 2, MidpointRounding.AwayFromZero));
    }

    public Amount Negate()
    {
        return new Amount(-Quantity);
    }

    public override string ToString()
    {
        return Quantity.ToString("0.00", CultureInfo.CreateSpecificCulture("nl-BE"));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Quantity;
    }
}