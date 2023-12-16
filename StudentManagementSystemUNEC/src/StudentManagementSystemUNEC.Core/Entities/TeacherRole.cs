using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class TeacherRole : BaseSectionEntity
{
    public TeacherRoleName Name { get; set; }
    public ICollection<TeacherSubject> teacherSubjects { get; set; }
}   