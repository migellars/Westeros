namespace Domain.Models.Helper.ValueObject;

public class Address: ValueObject
{
    public string Street { get;  init; }
    public string City { get;  init; }
    public string State { get;  init; }
    public string Country { get;  init; }
    public string ZipCode { get;  init; }
    public string Lga { get;  init; }

    public Address()
    {
        
    }
    public Address(string street, string city, string state, string country, string zipcode, string lga)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
        Lga = lga;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
        yield return Lga;
    }
}