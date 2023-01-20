using Domain.Models.Helper.ValueObject;

namespace Application.Dto;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    private string City { get; set; }
    private string Street { get; set; }
    private string State { get; set; }
    private string Country { get; set; }
    private string LocalGovt { get; set; }
    private string ZipCode { get; set; }
    public Address Address { get; set; }

    public Address GetAddress()
    {
        return new Address(Street, City, State, Country, ZipCode, LocalGovt);
    }
}