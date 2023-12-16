using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystemUNEC.Business.DTOs.RoleDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public List<RoleGetDTO> GetRoles()
    {
        var roles = _roleManager.Roles.ToList();

        // Filter out the "Admin" role
        var rolesExceptAdmin = roles.Where(role => role.Name != "Admin").ToList();

        var roleGetDTOs = _mapper.Map<List<RoleGetDTO>>(rolesExceptAdmin);

        return roleGetDTOs;

    }
}