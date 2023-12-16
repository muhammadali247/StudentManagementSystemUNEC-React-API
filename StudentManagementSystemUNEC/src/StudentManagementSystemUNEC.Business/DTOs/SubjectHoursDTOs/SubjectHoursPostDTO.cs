using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemUNEC.Business.DTOs.SubjectHoursDTOs;

public class SubjectHoursPostDTO
{
    public DayOfWeek DayOfWeek { get; set; }
    public Guid LessonTypeId { get; set; }
    public string Room { get; set; }
    public Guid GroupSubjectId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}