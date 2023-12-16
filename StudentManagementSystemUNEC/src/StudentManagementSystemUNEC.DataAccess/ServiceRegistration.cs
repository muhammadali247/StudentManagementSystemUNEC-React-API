using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess;

public static class ServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<AppDbContextInitializer>();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IGroupSubjectRepository, GroupSubjectRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherRoleRepository, TeacherRoleRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
        services.AddScoped<ISubjectHourRepository, SubjectHourRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IExamTypeRepository, ExamTypeRepository>();
        services.AddScoped<IExamResultRepository, ExamResultRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}