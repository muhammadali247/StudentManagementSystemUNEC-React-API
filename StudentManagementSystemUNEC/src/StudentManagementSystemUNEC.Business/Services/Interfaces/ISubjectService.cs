using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ISubjectService
{
    public Task<List<SubjectGetDTO>> GetAllSubjectsAsync(string? search);
    public Task<SubjectGetDTO> GetSubjectByIdAsync(Guid Id);

    public Task CreateSubjectAsync(SubjectPostDTO subjectPostDTO);
    public Task DeleteSubjectAsync(Guid Id);
    public Task UpdateSubjectAsync(Guid Id, SubjectPutDTO subjectPutDTO);
    public Task<bool> CheckSubjectExistsByIdAsync(Guid Id);
}
