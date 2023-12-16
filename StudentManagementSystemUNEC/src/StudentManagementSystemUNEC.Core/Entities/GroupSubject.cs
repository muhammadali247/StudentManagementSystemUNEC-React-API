using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class GroupSubject : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; }
    public byte Credits { get; set; }
    public short totalHours { get; set; }
    public List<TeacherSubject>? teacherSubjects { get; set; }
}