using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.DataAccess.Configurations;

namespace StudentManagementSystemUNEC.DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
    }

    public DbSet<Student> Students { get; set; } = null!;
	public DbSet<StudentGroup> StudentGroups { get; set; } = null!;
	public DbSet<Group> Groups { get; set; } = null!;
	public DbSet<GroupSubject> GroupSubjects { get; set; } = null!;
	public DbSet<Subject> Subjects { get; set; } = null!;
	public DbSet<TeacherSubject> TeacherSubjects { get; set; } = null!;
	public DbSet<Teacher> Teachers { get; set; } = null!;
	public DbSet<TeacherRole> teacherRoles { get; set; } = null!;
	public DbSet<Faculty> Faculties { get; set; } = null!;
	public DbSet<LessonType> lessonTypes { get; set; } = null!;
    public DbSet<Exam> Exams { get; set; } = null!;
    public DbSet<ExamType> examTypes { get; set; } = null!;
    public DbSet<ExamResult> examResults { get; set; } = null!;

    public DbSet<RefreshToken> refreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
		modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<StudentGroup>().HasQueryFilter(sg => !sg.IsDeleted);
        modelBuilder.Entity<Group>().HasQueryFilter(g => !g.IsDeleted);
        modelBuilder.Entity<GroupSubject>().HasQueryFilter(gs => !gs.IsDeleted);
        modelBuilder.Entity<Subject>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<TeacherSubject>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Teacher>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<TeacherRole>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Faculty>().HasQueryFilter(f => !f.IsDeleted);
        modelBuilder.Entity<LessonType>().HasQueryFilter(lt => !lt.IsDeleted);
        modelBuilder.Entity<Exam>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ExamType>().HasQueryFilter(et => !et.IsDeleted);
        modelBuilder.Entity<ExamResult>().HasQueryFilter(er => !er.IsDeleted);

        modelBuilder.Entity<RefreshToken>().HasQueryFilter(rt => !rt.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
        string? name = "Admin";
        var identity = _contextAccessor?.HttpContext?.User.Identity;

        if (identity is not null)
        {
            name = identity.IsAuthenticated ? identity.Name : "Admin";
        }

		var entries = ChangeTracker.Entries<BaseSectionEntity>();
		foreach (var entry in entries)
		{
			switch (entry.State)
			{
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = name;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = name;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = name;
                    break;
                default:
                    break;
            }
		}
		return base.SaveChangesAsync(cancellationToken);
	}
}
