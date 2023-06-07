using Microsoft.EntityFrameworkCore;
using PAR.Domain.Entities;

namespace PAR.Application.DataAccessLayer;

public interface IParDbContext
{
    DbSet<SchoolYear> SchoolYears { get; set; }
    DbSet<ApplicationUser> ApplicationUsers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}