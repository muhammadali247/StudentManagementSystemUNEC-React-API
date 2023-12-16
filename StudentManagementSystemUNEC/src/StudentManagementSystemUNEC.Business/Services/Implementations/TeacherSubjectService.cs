using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class TeacherSubjectService : ITeacherSubjectService
{
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;
    public TeacherSubjectService(ITeacherSubjectRepository teacherSubjectRepository)
    {
        _teacherSubjectRepository = teacherSubjectRepository;
    }

    public async Task<List<TeacherSubject>> GetAllTeacherSubjectsAsync()
    {
        var teacherSubjects = await _teacherSubjectRepository.GetAll().ToListAsync();
        return teacherSubjects;
    }
    public async Task<List<TeacherSubject>> GetTeacherSubjectsForGroupSubjectAsync(Guid groupSubjectId)
    {
        var teacherSubjects = await _teacherSubjectRepository.GetFiltered(ts => ts.GroupSubjectId == groupSubjectId).ToListAsync();
        return teacherSubjects;
    }

    public async Task<TeacherSubject> GetGroupByIdAsync(Guid id)
    {
        var teacherSubject = await _teacherSubjectRepository.GetSingleAsync(ts => ts.Id == id);
        return teacherSubject;
    }

    public async Task DeleteTeacherSubjectsAsync(TeacherSubject teacherSubject)
    {
        _teacherSubjectRepository.Delete(teacherSubject);
        await _teacherSubjectRepository.SaveAsync();
    }

    public async Task DeleteTeacherSubjectsAsync(List<TeacherSubject> teacherSubjects)
    {
        _teacherSubjectRepository.DeleteList(teacherSubjects);
        await _teacherSubjectRepository.SaveAsync();
    }
}
