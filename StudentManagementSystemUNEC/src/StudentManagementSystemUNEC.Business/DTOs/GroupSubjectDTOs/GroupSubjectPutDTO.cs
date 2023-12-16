namespace StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

public class GroupSubjectPutDTO
{
    public Guid GroupId { get; set; }
    public Guid SubjectId { get; set; }
    public List<PostTeacherSubjectRoleDTO>? teacherRole { get; set; }
    public byte Credits { get; set; }
    public short totalHours { get; set; }
}