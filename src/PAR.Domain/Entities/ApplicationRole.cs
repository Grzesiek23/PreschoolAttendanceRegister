using Microsoft.AspNetCore.Identity;

namespace PAR.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}