using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAR.Domain.Entities;

namespace PAR.Persistence.Context;

public static class SeedContext
{
    public static async Task SeedDefaultData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var roles = await roleManager.FindByNameAsync("Admin");
        
        if (roles == null)
        {
            await roleManager.CreateAsync(new ApplicationRole {Name = "Admin"});
        }
        
        var users = await userManager.Users.ToListAsync();
        
        if (!users.Any())
        {
            var user = new ApplicationUser()
            {
                FirstName = "Greg",
                LastName = "Bad",
                Email = "admin@par.pl",
                UserName = "admin@par.pl",
                PhoneNumber = "123456789"
            };

            await userManager.CreateAsync(user, "Test123!");
            
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}