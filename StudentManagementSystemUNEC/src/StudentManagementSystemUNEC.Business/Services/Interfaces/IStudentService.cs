using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IStudentService
{
    public Task<List<StudentGetDTO>> GetAllStudentsAsync(string? search);
    public Task<StudentGetDTO> GetStudentByIdAsync(Guid Id);
    public Task CreateStudentAsync(StudentPostDTO StudentPostDTO);
    public Task DeleteStudentAsync(Guid Id);
    public Task UpdateStudentAsync(Guid Id, StudentPutDTO StudentPutDTO);
    public Task<bool> CheckStudentExistsByIdAsync(Guid Id);
}
