using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Group : BaseSectionEntity
{
    public string Name { get; set; }
    public byte StudentCount { get; set; }
    public DateTime CreationYear { get; set; }
    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<Student>? Students { get; set; }
    public ICollection<StudentGroup>? StudentGroups { get; set; }
    public ICollection<GroupSubject>? GroupSubjects { get; set; }
}
