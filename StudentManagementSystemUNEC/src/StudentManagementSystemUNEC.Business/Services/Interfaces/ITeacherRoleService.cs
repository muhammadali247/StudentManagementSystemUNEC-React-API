using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ITeacherRoleService 
{
    public Task<List<TeacherRoleGetDTO>> GetAllTeacherRolesAsync(string? search);
    public Task<TeacherRoleGetDTO> GetTeacherRoleByIdAsync(Guid Id);
    public Task CreateTeacherRoleAsync(TeacherRolePostDTO teacherRolePostDTO);
    public Task DeleteTeacherRoleAsync(Guid Id);
    public Task UpdateTeacherRoleAsync(Guid Id, TeacherRolePutDTO teacherRolePutDTO);
    public Task<bool> CheckTeacherRoleExistsByIdAsync(Guid Id);
}