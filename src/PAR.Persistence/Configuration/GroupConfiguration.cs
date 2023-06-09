using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAR.Domain.Entities;

namespace PAR.Persistence.Configuration;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        
        builder.HasOne(x => x.SchoolYear)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.SchoolYearId);
        
        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.TeacherId);
    }
}