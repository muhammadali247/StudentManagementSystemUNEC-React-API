using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.Property(t => t.Image).IsRequired(true);
        builder.Property(t => t.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(t => t.Surname).IsRequired(true).HasMaxLength(50);
        builder.Property(t => t.middleName).IsRequired(true).HasMaxLength(50);
        builder.Property(t => t.Gender).IsRequired(true).HasConversion<string>();
        builder.Property(t => t.Country).IsRequired(true).HasConversion<string>();
        builder.Property(t => t.BirthDate).IsRequired(false);

        //builder.HasMany(s => s.studentGroups).WithOne(sg => sg.Student);
    }
}
