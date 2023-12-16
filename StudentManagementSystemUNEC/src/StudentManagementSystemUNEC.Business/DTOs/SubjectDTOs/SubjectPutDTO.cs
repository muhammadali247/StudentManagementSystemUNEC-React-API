using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;

public class SubjectPutDTO
{
    public string Name { get; set; }
    public string subjectCode { get; set; }
    public Semester Semester { get; set; }
}