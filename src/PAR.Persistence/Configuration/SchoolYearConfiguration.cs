using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAR.Domain.Entities;

namespace PAR.Persistence.Configuration;

public class SchoolYearConfiguration : IEntityTypeConfiguration<SchoolYear>
{
    public void Configure(EntityTypeBuilder<SchoolYear> builder)
    {
        builder.Property(e => e.StartDate)
            .HasColumnType("date")
            .HasConversion(v => v.ToDateTime(default), v => DateOnly.FromDateTime(v));
        builder.Property(e => e.EndDate)
            .HasColumnType("date")
            .HasConversion(v => v.ToDateTime(default), v => DateOnly.FromDateTime(v));
    }
}