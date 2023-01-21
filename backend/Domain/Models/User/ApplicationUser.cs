using Microsoft.AspNetCore.Identity;

namespace Domain.Models.User;

public class ApplicationUser: IdentityUser<Guid> 
{
    public int UserType { get; set; }
}