using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Teacher : BaseSectionEntity
{
    public string Image { get; set; }
    public AppUser? AppUser { get; set; }
    public string? AppUserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    public Gender Gender { get; set; }
    public Country Country { get; set; }
    public DateTime? BirthDate { get; set; }
    public ICollection<TeacherSubject>? teacherSubjects { get; set; }
}