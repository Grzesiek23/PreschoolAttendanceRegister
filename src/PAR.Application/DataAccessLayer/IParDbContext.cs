using Microsoft.EntityFrameworkCore;
using PAR.Domain.Entities;

namespace PAR.Application.DataAccessLayer;

public interface IParDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; set; }
    DbSet<Preschooler> Preschoolers { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<SchoolYear> SchoolYears { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}