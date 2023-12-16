using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ITeacherService
{
    public Task<List<TeacherGetDTO>> GetAllTeachersAsync(string? search);
    public Task<TeacherGetDTO> GetTeacherByIdAsync(Guid Id);
    public Task CreateTeacherAsync(TeacherPostDTO teacherPostDTO);
    public Task DeleteTeacherAsync(Guid Id);
    public Task UpdateTeacherAsync(Guid Id, TeacherPutDTO teacherPutDTO);
    public Task<bool> CheckTeacherExistsByIdAsync(Guid Id);
}
