using ValueObjects.ValueObjects;

namespace ValueObjects.UnitTests.ValueObjects;

public sealed class ChildBuilder
{
    private ChildNr _nr = new(1);
    private CustomerNr _customerNr = new(123);

    private ChildName _name = new("Last name", "First name");
    private Birthdate _birthdate = new(new DateOnly(2022, 7, 15));

    private NationalNumber _nationalNumber = new("22071501377");

    public ChildBuilder WithNr(ChildNr nr)
    {
        _nr = nr;
        return this;
    }

    public ChildBuilder WithCustomerNr(CustomerNr customerNr)
    {
        _customerNr = customerNr;
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
            _nr,
            _customerNr,
            _name,
            _birthdate,
            _nationalNumber
        );
    }
}