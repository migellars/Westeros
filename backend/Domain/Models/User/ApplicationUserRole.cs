using Microsoft.AspNetCore.Identity;

namespace Domain.Models.User;

public class ApplicationUserRole: IdentityUserRole<Guid>
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; }
    public bool IsActive { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}