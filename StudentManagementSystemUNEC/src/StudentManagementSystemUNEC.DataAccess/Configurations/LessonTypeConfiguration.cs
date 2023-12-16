using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class LessonTypeConfiguration : IEntityTypeConfiguration<LessonType>
{
    public void Configure(EntityTypeBuilder<LessonType> builder)
    {
        builder.Property(tr => tr.Name).IsRequired(true).HasMaxLength(128);
    }
}