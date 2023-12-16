using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Image).IsRequired(true);
        //builder.Property(s => s.Email).IsRequired(true).HasMaxLength(255);
        //builder.Property(s => s.Username).IsRequired(true).HasMaxLength(255);
        //builder.Property(s => s.Password).IsRequired(true).HasMaxLength(50);
        builder.Property(s => s.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(s => s.Surname).IsRequired(true).HasMaxLength(50);
        builder.Property(s => s.middleName).IsRequired(true).HasMaxLength(50);
        builder.Property(s => s.admissionYear).IsRequired(true);

        //builder.Property(s => s.educationStatus).IsRequired(true);
        builder.Property(s => s.educationStatus).IsRequired(true).HasConversion<string>();

        //builder.Property(s => s.Gender).IsRequired(true);
        builder.Property(s => s.Gender).IsRequired(true).HasConversion<string>();

        //builder.Property(s => s.Country).IsRequired(false).HasMaxLength(50);
        //builder.Property(s => s.Country).IsRequired(false).HasMaxLength(50).HasConversion<string>();
        builder.Property(s => s.Country).IsRequired(true).HasMaxLength(50).HasConversion<string>();

        builder.Property(s => s.BirthDate).IsRequired(false);
        builder.Property(s => s.corporativeEmail).IsRequired(false).HasMaxLength(255);
        builder.Property(s => s.corporativePassword).IsRequired(false).HasMaxLength(50);

        builder.HasMany(s => s.studentGroups).WithOne(sg => sg.Student);
    }

}