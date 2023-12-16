using StudentManagementSystemUNEC.Business.DTOs.LessonTypeDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ILessonTypeService
{
    public Task<List<LessonTypeGetDTO>> GetAllLessonTypesAsync(string? search);
    public Task<LessonTypeGetDTO> GetLessonTypeByIdAsync(Guid Id);
    public Task CreateLessonTypeAsync(LessonTypePostDTO lessonTypePostDTO);
    public Task DeleteLessonTypeAsync(Guid Id);
    public Task UpdateLessonTypeAsync(Guid Id, LessonTypePutDTO lessonTypePutDTO);
    public Task<bool> CheckLessonTypeExistsByIdAsync(Guid Id);
}