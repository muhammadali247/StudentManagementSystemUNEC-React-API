using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(g => g.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(g => g.StudentCount).IsRequired(true);
        builder.Property(g => g.CreationYear).IsRequired(true);

        builder.HasMany(g => g.StudentGroups).WithOne(gs => gs.Group);
        builder.HasOne(g => g.Faculty).WithMany(f => f.Groups);
    }

}