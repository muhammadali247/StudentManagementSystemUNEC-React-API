using Microsoft.AspNetCore.Identity;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.DataAccess.Contexts;

namespace StudentManagementSystemUNEC.API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.AllowedForNewUsers = false;

            //options.SignIn.RequireConfirmedEmail = false;
            //options.SignIn.RequireConfirmedPhoneNumber = false;
            //options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;

        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}