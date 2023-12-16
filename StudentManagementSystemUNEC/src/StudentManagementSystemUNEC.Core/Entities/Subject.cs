using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Subject : BaseSectionEntity
{
    public string Name { get; set; }
    public string subjectCode { get; set; }
    public Semester Semester { get; set; }
    public ICollection<GroupSubject>? GroupSubjects { get; set; }
}
