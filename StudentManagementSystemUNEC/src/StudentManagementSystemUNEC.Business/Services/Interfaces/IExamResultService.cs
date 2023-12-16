using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IExamResultService
{
    Task<List<ExamResultGetDTO>> GetAllExamResultsAsync(string? studentName);
    Task<ExamResultGetDTO> GetExamResultByIdAsync(Guid id);
    Task CreateExamResultAsync(ExamResultPostDTO examResultPostDTO);
    Task UpdateExamResultAsync(Guid id, ExamResultPutDTO putExamResultDTO);
    Task DeleteExamResultAsync(Guid id);
}