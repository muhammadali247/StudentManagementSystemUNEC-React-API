using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class ExamTypeCofiguration : IEntityTypeConfiguration<ExamType>
{
    public void Configure(EntityTypeBuilder<ExamType> builder)
    {
        builder.Property(et => et.Name).IsRequired(true).HasMaxLength(128).HasConversion<string>();
        //builder.Property(et => et.Name).IsRequired(true).HasMaxLength(128);
        builder.Property(e => e.maxScore).IsRequired();

        // Add check constraint for maxScore not exceeding 100
        builder.HasCheckConstraint("maxScore", "maxScore <= 100");
    }
}