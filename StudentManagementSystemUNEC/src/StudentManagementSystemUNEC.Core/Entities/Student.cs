using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Student : BaseSectionEntity
{
    public string Image { get; set; }
    public AppUser? AppUser { get; set; }
    public string? AppUserId { get; set; }
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
    public Group? Group { get; set; }
    public Guid? GroupId { get; set; }
    public List<StudentGroup>? studentGroups { get; set; }
    public List<ExamResult>? examResults { get; set; }
}