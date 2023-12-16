using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.LessonTypeDTOs;

public class LessonTypeGetDTO
{
    public Guid Id { get; set; }
    public LessonTypeName Name { get; set; }
}