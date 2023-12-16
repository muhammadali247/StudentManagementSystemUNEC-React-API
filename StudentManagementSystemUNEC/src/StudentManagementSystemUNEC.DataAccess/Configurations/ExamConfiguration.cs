using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.GroupSubjectId).IsRequired();
        builder.Property(e => e.ExamTypeId).IsRequired();
        //builder.Property(e => e.maxScore).IsRequired();

        //builder.HasMany(e => e.)
    }
}