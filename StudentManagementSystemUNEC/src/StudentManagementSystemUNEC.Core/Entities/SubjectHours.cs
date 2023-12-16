using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class SubjectHours : BaseSectionEntity
{
    public GroupSubject GroupSubject { get; set; }
    public Guid GroupSubjectId { get; set; }
    public LessonType LessonType { get; set; }
    public Guid LessonTypeId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string Room { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}