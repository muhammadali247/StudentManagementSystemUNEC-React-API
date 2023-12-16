using StudentManagementSystemUNEC.Business.DTOs.FacultyDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IFacultyService
{
    public Task<List<FacultyGetDTO>> GetAllFacultiesAsync(string? search);
    public Task<FacultyGetDTO> GetFacultyByIdAsync(Guid Id);
    public Task CreateFacultyAsync(FacultyPostDTO facultyPostDTO);
    public Task DeleteFacultyAsync(Guid Id);
    public Task UpdateFacultyAsync(Guid Id, FacultyPutDTO facultyPutDTO);
    public Task<bool> CheckFacultyExistsByIdAsync(Guid Id);
}
