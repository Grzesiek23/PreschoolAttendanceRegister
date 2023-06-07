using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAR.Domain.Entities;

namespace PAR.Persistence.Configuration;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("ApplicationUserRoles");
        builder.HasKey(e => new { e.UserId, e.RoleId });
            
        builder.HasOne(d => d.ApplicationRole)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.RoleId);

        builder.HasOne(d => d.User)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.UserId);
    }
}