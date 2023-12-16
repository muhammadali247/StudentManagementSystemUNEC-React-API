using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;

public class TeacherRoleGetDTO
{
    public Guid Id { get; set; }
    //public string Name { get; set; }
    public LessonTypeName Name { get; set; }
}