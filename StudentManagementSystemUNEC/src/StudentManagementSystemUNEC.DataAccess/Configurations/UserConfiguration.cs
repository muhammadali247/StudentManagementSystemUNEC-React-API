using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Primary Key
        builder.HasKey(u => u.Id);

        // User Name
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(256);

        // Email
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        // Index on Email
        builder.HasIndex(u => u.Email).IsUnique();

        // Set the table name (if it's different from the entity name)
        builder.ToTable("AppUsers");
    }
}