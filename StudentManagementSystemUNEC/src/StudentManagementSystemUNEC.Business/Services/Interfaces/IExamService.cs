using StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IExamService
{
    Task<List<ExamGetDTO>> GetAllExamsAsync(string? search);
    Task<ExamGetDTO> GetExamByIdAsync(Guid id);
    Task CreateExamAsync(ExamPostDTO examPostDTO);
    Task DeleteExamAsync(Guid id);
    Task UpdateExamAsync(Guid id, ExamPutDTO examPutDTO);
}