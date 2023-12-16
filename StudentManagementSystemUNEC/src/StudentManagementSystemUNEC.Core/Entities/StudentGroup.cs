using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class StudentGroup : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public SubGroup subGroup { get; set; }
}
