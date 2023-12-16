using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class TeacherSubject : BaseSectionEntity
{
    public Teacher Teacher { get; set; }
    public Guid TeacherId { get; set; }
    public GroupSubject GroupSubject { get; set; }
    public Guid GroupSubjectId { get; set; }
    public TeacherRole TeacherRole { get; set; }
    public Guid TeacherRoleId { get; set; }
}
