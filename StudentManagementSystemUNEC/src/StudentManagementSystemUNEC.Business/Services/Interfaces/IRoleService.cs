

using StudentManagementSystemUNEC.Business.DTOs.RoleDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IRoleService
{
    List<RoleGetDTO> GetRoles();
}