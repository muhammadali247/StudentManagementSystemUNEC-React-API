using StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IExamTypeService
{
    public Task<List<ExamTypeGetDTO>> GetAllExamTypesAsync(string? search);
    public Task<ExamTypeGetDTO> GetExamTypeByIdAsync(Guid Id);
    public Task CreateExamTypeAsync(ExamTypePostDTO examTypePostDTO);
    public Task DeleteExamTypeAsync(Guid Id);
    public Task UpdateExamTypeAsync(Guid Id, ExamTypePutDTO examTypePutDTO);
    public Task<bool> CheckExamTypeExistsByIdAsync(Guid Id);
}