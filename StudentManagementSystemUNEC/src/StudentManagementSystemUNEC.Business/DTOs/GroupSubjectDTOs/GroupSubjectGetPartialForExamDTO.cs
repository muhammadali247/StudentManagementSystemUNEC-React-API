namespace StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

public class GroupSubjectGetPartialForExamDTO
{
    public Guid Id { get; set; }
    public string groupName { get; set; }
    public string subjectName { get; set; }
    public byte Credits { get; set; }
}