using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagementSystemUNEC.Business.HelperServices.BakcgroundServices;
using StudentManagementSystemUNEC.Business.HelperServices.Implementations;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.MappingProfiles;
using StudentManagementSystemUNEC.Business.Services.Implementations;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Business.Validators.StudentValidators;

namespace StudentManagementSystemUNEC.Business;

public static class ServiceRegistration
{
    public static IServiceCollection AddBuinessServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IGroupSubjectService, GroupSubjectService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ITeacherSubjectService, TeacherSubjectService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ITeacherRoleService, TeacherRoleService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<ILessonTypeService, LessonTypeService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IExamTypeService, ExamTypeService>();
        services.AddScoped<IExamResultService, ExamResultService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        //services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IFileReadService, FileReadService>();
        services.AddTransient<IEmailService, EmailService>();
        //services.AddScoped<IOTPService, OTPService>();
        services.AddTransient<OTPService>();

        services.AddHostedService<ExpiredOTPCleanupService>();
        services.AddHostedService<ExpiredRefreshTokenCleanupService>();

        services.AddAutoMapper(typeof(StudentMappingProfile).Assembly);

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(StudentPostDTOValidator).Assembly));

        return services;
    }
}