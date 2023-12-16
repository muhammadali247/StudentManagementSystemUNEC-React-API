using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

public class GroupGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public byte StudentCount { get; set; }
    public DateTime CreationYear { get; set; }
    public string facultyName { get; set; }
    public ICollection<StudentGetPartialDTO>? Students { get; set; }
    public ICollection<GroupSubjectGetPartialForGroupDTO>? GroupSubjects { get; set; }

}
