using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.Property(g => g.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(g => g.subjectCode).IsRequired(true).HasMaxLength(6);
        builder.Property(g => g.Semester).IsRequired(true).HasConversion<string>();

        builder.HasMany(g => g.GroupSubjects).WithOne(gs => gs.Subject);
    }
}
