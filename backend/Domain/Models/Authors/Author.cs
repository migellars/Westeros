using Domain.Models.Helper.ValueObject;
using Domain.Models.Lesson;

namespace Domain.Models.Authors;

public class Author: BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address? Address { get; set; }
    public virtual ICollection<Tutorial> Tutorials { get; set; }
    public Author()
    {
        
    }
    private Author(string firstName, string lastName, string email, string phone, Address address)
    {
        Email = email;
        Phone = phone;
        Address = address;
        FirstName = firstName;
        LastName = lastName;
    }
}