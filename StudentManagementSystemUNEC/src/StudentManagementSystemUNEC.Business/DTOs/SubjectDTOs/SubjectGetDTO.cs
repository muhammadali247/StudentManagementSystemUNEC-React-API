using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;

public class SubjectGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SubjectCode { get; set; }
    public Semester Semester { get; set; }
}