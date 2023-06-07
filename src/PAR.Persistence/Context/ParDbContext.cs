using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Domain.Entities;

namespace PAR.Persistence.Context;

public class ParDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IParDbContext
{
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public ParDbContext(DbContextOptions options) : base(options)
    {
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
}