using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class SubjectHoursConfiguration : IEntityTypeConfiguration<SubjectHours>
{
    public void Configure(EntityTypeBuilder<SubjectHours> builder)
    {
        builder.Property(e => e.GroupSubjectId).IsRequired();
        builder.Property(e => e.LessonTypeId).IsRequired();
        builder.Property(e => e.DayOfWeek).IsRequired();
        builder.Property(e => e.Room).IsRequired().HasMaxLength(255); // Adjust the maximum length as needed
        builder.Property(e => e.StartTime).IsRequired();
        builder.Property(e => e.EndTime).IsRequired();

        //////////////////////////////////////////////////////////////////////////
        builder.HasOne(e => e.GroupSubject)
                .WithMany()
                .HasForeignKey(e => e.GroupSubjectId);
        builder.HasOne(e => e.LessonType)
            .WithMany()
            .HasForeignKey(e => e.LessonTypeId);
    }
}