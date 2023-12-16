using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

public class GroupSubjectGetPartialForGroupDTO
{
    public Guid Id { get; set; }
    public SubjectGetDTO Subject { get; set; }
    public List<TeacherGetPartialDTO>? Teachers { get; set; }
    public byte Credits { get; set; }
    public short totalHours { get; set; }
}