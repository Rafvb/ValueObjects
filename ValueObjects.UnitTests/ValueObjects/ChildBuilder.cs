using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class ChildBuilder
{
    private ChildId _id = new(1);
    private CustomerId _customerId = new(123);

    private ChildName _name = new("Last name", "First name");
    private Birthdate _birthdate = new(new DateOnly(2022, 7, 15));

    private NationalNumber _nationalNumber = new("22071501377");

    public ChildBuilder WithId(ChildId id)
    {
        _id = id;
        return this;
    }

    public ChildBuilder WithCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public ChildBuilder WithName(ChildName name)
    {
        _name = name;
        return this;
    }

    public ChildBuilder WithBirthdate(Birthdate birthdate)
    {
        _birthdate = birthdate;
        return this;
    }

    public ChildBuilder WithNationalNumber(NationalNumber nationalNumber)
    {
        _nationalNumber = nationalNumber;
        return this;
    }

    public Child Build()
    {
        return new Child(
            _id,
            _customerId,
            _name,
            _birthdate,
            _nationalNumber
        );
    }
}