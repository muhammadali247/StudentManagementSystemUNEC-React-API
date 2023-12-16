using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> builder)
    {
        builder.Property(er => er.ExamId).IsRequired(true);
        builder.Property(er => er.StudentId).IsRequired(true);
        builder.Property(er => er.Score).IsRequired(true);
    }
}