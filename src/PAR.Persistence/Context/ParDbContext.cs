using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Domain.Common;
using PAR.Domain.Entities;

namespace PAR.Persistence.Context;

public class ParDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IParDbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Preschooler> Preschoolers { get; set; }

    private readonly IHttpContextAccessor _httpContextAccessor;
    public ParDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("ApplicationUserClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("ApplicationUserLogins");
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
        });
        
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("ApplicationRoleClaims");
        });
        
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("ApplicationUserTokens");
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            entity.Property(e => e.LoginProvider).HasMaxLength(128);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changedByFullName = _httpContextAccessor.HttpContext?.User?.FindFirstValue("fullName");
        var currentUserEmail = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var changedBy = string.IsNullOrEmpty($"{changedByFullName} {currentUserEmail}") ? "System" : $"{changedByFullName} {currentUserEmail}";

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = changedBy;
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.IsActive = true;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = changedBy;
                    entry.Entity.LastModifiedOn = DateTime.Now;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.InactivatedBy = changedBy;
                    entry.Entity.InactivatedOn = DateTime.Now;
                    entry.Entity.IsActive = false;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}