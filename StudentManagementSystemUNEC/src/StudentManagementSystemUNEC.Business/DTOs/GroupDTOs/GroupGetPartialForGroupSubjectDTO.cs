using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

public class GroupGetPartialForGroupSubjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationYear { get; set; }
    public string FacultyName { get; set; }
    public List<StudentGetPartialDTO>? Students { get; set; }
}