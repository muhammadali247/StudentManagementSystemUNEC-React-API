using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.Property(f => f.Name).IsRequired().HasMaxLength(250);
        //builder.Property(f => f.studySector).IsRequired(true);
        builder.Property(f => f.facultyCode).IsRequired(true);

        builder.HasMany(f => f.Groups).WithOne(g => g.Faculty);
    }
}
