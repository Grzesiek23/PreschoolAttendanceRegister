using Microsoft.AspNetCore.Identity;

namespace PAR.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
}