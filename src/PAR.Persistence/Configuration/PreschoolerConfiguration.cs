using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAR.Domain.Entities;

namespace PAR.Persistence.Configuration;

public class PreschoolerConfiguration : IEntityTypeConfiguration<Preschooler>
{
    public void Configure(EntityTypeBuilder<Preschooler> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(25);
        builder.Property(e => e.LastName).HasMaxLength(50);
        
        builder.HasOne(d => d.Group)
            .WithMany(p => p.Preschoolers)
            .HasForeignKey(d => d.GroupId);
    }
}