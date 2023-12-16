using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.DataAccess.Contexts;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
    }

    public async Task UserSeedAsync()
    {
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        }

        var admin = new AppUser()
        {
            IsActive = true,
            UserName = "Admin",
            EmailConfirmed = true,
            Email = "magoo.everywhere123@gmail.com"
        };
        await _userManager.CreateAsync(admin, "Salam12345!");
        await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
    }
}