using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ITeacherSubjectService
{
    Task<List<TeacherSubject>> GetAllTeacherSubjectsAsync();
    Task<TeacherSubject> GetGroupByIdAsync(Guid id);
    Task<List<TeacherSubject>> GetTeacherSubjectsForGroupSubjectAsync(Guid groupSubjectId);
    Task DeleteTeacherSubjectsAsync(List<TeacherSubject> teacherSubjects);
}
