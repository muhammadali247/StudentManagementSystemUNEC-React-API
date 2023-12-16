using Microsoft.AspNetCore.Http;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;

public class StudentPutDTO
{
    public IFormFile? Image { get; set; }
    //public string? AppUserId { get; set; }
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
    public Guid? MainGroup { get; set; }
    public List<Guid>? GroupIds { get; set; }
    public List<SubGroup> subGroups { get; set; }
}