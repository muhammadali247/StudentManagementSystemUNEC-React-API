using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

public class GroupSubjectGetPartialForTeacherDTO
{
    public GroupGetPartialForTeacherDTO Group { get; set; }
    public string subjectName { get; set; }
    public byte Credits { get; set; }
    public short totalHours { get; set; }
}
