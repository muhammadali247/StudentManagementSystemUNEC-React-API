using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

public class StudentGetDTO
{
    public Guid Id { get; set; }
    public string Image { get; set; }
    //public string Email { get; set; }
    //public string Username { get; set; }
    //public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    public DateTime admissionYear { get; set; }
    //public string educationStatus { get; set; }
    public EducationStatus educationStatus { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? corporativeEmail { get; set; }
    public string? corporativePassword { get; set; }
    //public string Gender { get; set; }
    public Gender Gender { get; set; }
    //public string Country { get; set; }
    public Country Country { get; set; }
    public MainGroupGetPartialForStudentDTO? MainGroup { get; set; }
    public string MainGroupName { get; set; }
    public UserGetDTO? AppUser { get; set; }
    public string Username { get; set; }
    public List<ExamResultGetDTO>? examResults { get; set; }
    public List<GroupGetPartialDTO>? Groups { get; set; }
    public ICollection<string> GroupNames { get; set; }
}