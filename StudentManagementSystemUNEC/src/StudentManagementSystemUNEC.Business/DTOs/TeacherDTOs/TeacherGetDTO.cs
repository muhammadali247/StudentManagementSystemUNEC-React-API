using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;

public class TeacherGetDTO
{
    public Guid Id { get; set; }
    public string Image { get; set; }
    public UserGetDTO? AppUser { get; set; }
    public string Username { get; set; }
    //public string Email { get; set; }
    //public string Username { get; set; }
    //public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string middleName { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public DateTime? BirthDate { get; set; }
}