using Microsoft.AspNetCore.Http;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

public class TeacherPostDTO
{
    public IFormFile? Image { get; set; }
    public string? AppUserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    public Gender Gender { get; set; }
    public Country Country { get; set; }
    public DateTime? BirthDate { get; set; }
}