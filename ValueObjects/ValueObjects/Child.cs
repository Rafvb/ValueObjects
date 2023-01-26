using ValueObjects.Common;

namespace ValueObjects.ValueObjects;

public sealed class Child
{
    public Child(ChildNr nr, CustomerNr customerNr, ChildName name, Birthdate birthdate, NationalNumber nationalNumber)
    {
        Nr = nr ?? throw new BusinessRuleException("ChildNr should not be empty");

        if (customerNr == null || customerNr.IsEmpty)
        {
            throw new BusinessRuleException("CustomerNr should not be empty");
        }

        CustomerNr = customerNr;

        ChangeName(name);
        ChangeBirthdate(birthdate);
        ChangeNationalNumber(nationalNumber);
    }

    public ChildNr Nr { get; }
    public CustomerNr CustomerNr { get; }

    public ChildName Name { get; private set; }
    public Birthdate Birthdate { get; private set; }

    public NationalNumber NationalNumber { get; private set; }

    public void Change(ChildName name, Birthdate birthdate, NationalNumber nationalNumber)
    {
        ChangeName(name);
        ChangeBirthdate(birthdate);
        ChangeNationalNumber(nationalNumber);
    }

    private void ChangeName(ChildName name)
    {
        Name = name ?? throw new BusinessRuleException("Name should not be empty");
    }

    private void ChangeBirthdate(Birthdate birthdate)
    {
        Birthdate = birthdate ?? throw new BusinessRuleException("Birthdate should not be empty");
    }

    private void ChangeNationalNumber(NationalNumber nationalNumber)
    {
        if (nationalNumber == null)
        {
            throw new BusinessRuleException("National number should not be empty");
        }

        if (nationalNumber != NationalNumber.Empty)
        {
            if (Birthdate != Birthdate.Unknown && nationalNumber.Birthdate != Birthdate)
            {
                throw new BusinessRuleException($"Birthdate {Birthdate} does not match birthdate in National number {nationalNumber}");
            }

            // Validate gender
        }

        NationalNumber = nationalNumber;
    }
}