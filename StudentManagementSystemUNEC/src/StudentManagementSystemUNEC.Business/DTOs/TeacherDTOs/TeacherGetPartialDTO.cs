using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

public class TeacherGetPartialDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public TeacherRoleName RoleName { get; set; }
}
