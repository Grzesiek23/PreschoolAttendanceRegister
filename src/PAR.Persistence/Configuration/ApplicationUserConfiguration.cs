using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAR.Domain.Entities;

namespace PAR.Persistence.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(name: "ApplicationUsers");
        builder.Property(e => e.FirstName).HasMaxLength(25);
        builder.Property(e => e.LastName).HasMaxLength(50);
    }
}