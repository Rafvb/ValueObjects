using FluentAssertions.Extensions;
using FluentAssertions;
using ValueObjects.Common;
using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class DateRangeTests
{
    public static IEnumerable<object[]> InvalidInputData =>
    [
        [new DateTime(2024, 01, 02), new DateTime(2024, 01, 01)],
        [new DateTime(2024, 01, 03), new DateTime(2024, 01, 01)],
    ];

    public static IEnumerable<object[]> ValidInputData =>
    [
        [new DateTime(2024, 01, 01), new DateTime(2024, 01, 01)],
        [new DateTime(2024, 01, 01), new DateTime(2024, 01, 02)],
    ];

    public static IEnumerable<object[]> OverlapsWithData =>
    [
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 8, 0, 0), new DateTime(2024, 01, 01, 10, 0, 0)),
            true
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 8, 30, 0), new DateTime(2024, 01, 01, 9, 30, 0)),
            true
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 7, 0, 0), new DateTime(2024, 01, 01, 9, 0, 0)),
            true
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 9, 0, 0), new DateTime(2024, 01, 01, 11, 0, 0)),
            true
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 7, 0, 0), new DateTime(2024, 01, 01, 8, 0, 0)),
            false
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 01, 10, 0, 0), new DateTime(2024, 01, 01, 11, 0, 0)),
            false
        ],
        [
            new DateTime(2024, 01, 01, 8, 0, 0),
            new DateTime(2024, 01, 01, 10, 0, 0),
            new DateRange(new DateTime(2024, 01, 02, 8, 0, 0), new DateTime(2024, 01, 02, 10, 0, 0)),
            false
        ],
    ];

    public static IEnumerable<object[]> DurationData => new List<object[]>
    {
        new object[]
        {
            new DateTime(2024, 11, 19, 8, 0, 0), new DateTime(2024, 11, 19, 12, 0, 0), new TimeSpan(4, 0, 0),
        },
        new object[]
        {
            new DateTime(2024, 11, 19, 8, 0, 0), new DateTime(2024, 11, 20, 12, 0, 0), new TimeSpan(28, 0, 0),
        },
        new object[]
        {
            new DateTime(2024, 11, 19, 8, 30, 0), new DateTime(2024, 11, 19, 12, 0, 0), new TimeSpan(3, 30, 0),
        },
        new object[]
        {
            new DateTime(2024, 11, 19, 8, 30, 0), new DateTime(2024, 11, 19, 12, 11, 0), new TimeSpan(3, 41, 0),
        },
    };

    [Theory]
    [MemberData(nameof(ValidInputData))]
    public void Create_WithValidInput_CreatesDateRange(DateTime from, DateTime to)
    {
        var dateRange = new DateRange(from, to);

        dateRange.From.Should().Be(from);
        dateRange.To.Should().Be(to);
    }

    [Theory]
    [MemberData(nameof(InvalidInputData))]
    public void Create_WithInvalidInput_ThrowsBusinessRuleException(DateTime from, DateTime to)
    {
        FluentActions.Invoking(() => new DateRange(from, to))
            .Should()
            .Throw<BusinessRuleException>()
            .WithMessage("From should be same as or before Till");
    }

    [Theory]
    [MemberData(nameof(OverlapsWithData))]
    public void OverlapsWith_ReturnsTrueIfOverlap(
        DateTime from,
        DateTime to,
        DateRange otherDateRange,
        bool overlap)
    {
        new DateRange(from, to).OverlapsWith(otherDateRange).Should().Be(overlap);
    }

    [Fact]
    public void OverlapsWith_WithDateRangeNull_ThrowsBusinessRuleException()
    {
        FluentActions
            .Invoking(
                () => new DateRange(new DateTime(2024, 01, 01), new DateTime(2024, 01, 02)).OverlapsWith(null))
            .Should()
            .Throw<BusinessRuleException>()
            .WithMessage("Other DateRange should not be null");
    }

    [Theory]
    [InlineData(2024, 1, 2, true)]
    [InlineData(2024, 1, 3, true)]
    [InlineData(2024, 1, 4, true)]
    [InlineData(2024, 1, 1, false)]
    [InlineData(2025, 1, 5, false)]
    [InlineData(2025, 2, 2, false)]
    public void Contains_ReturnsTrueIfDateFallsInDateRange(int year, int month, int day, bool expected)
    {
        var dateRange = new DateRange(new DateTime(2024, 01, 02), new DateTime(2025, 01, 04));

        dateRange.Contains(new DateTime(year, month, day)).Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(DurationData))]
    public void Duration_ReturnsCorrectNumberOfHoursInDateRange(DateTime from, DateTime to, TimeSpan expectedDuration)
    {
        new DateRange(from, to).Duration.Should().Be(expectedDuration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(29)]
    public void Extend_ReturnsExtendedDateRange(int daysToExtend)
    {
        var dateRange = new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        var result = dateRange.Extend(TimeSpan.FromDays(daysToExtend));

        result.From.Should().Be(dateRange.From);
        result.To.Should().Be(dateRange.To + daysToExtend.Days());
    }
}