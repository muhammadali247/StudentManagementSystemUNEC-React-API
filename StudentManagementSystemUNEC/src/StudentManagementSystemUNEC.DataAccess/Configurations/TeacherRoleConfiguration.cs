using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class TeacherRoleConfiguration : IEntityTypeConfiguration<TeacherRole>
{
    public void Configure(EntityTypeBuilder<TeacherRole> builder)
    {
        builder.Property(tr => tr.Name).IsRequired(true).HasMaxLength(128).HasConversion<string>();

        builder.HasMany(tr => tr.teacherSubjects).WithOne(ts => ts.TeacherRole);
    }
}