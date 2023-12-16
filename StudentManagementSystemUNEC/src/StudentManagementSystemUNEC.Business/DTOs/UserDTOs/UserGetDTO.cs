namespace StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

public class UserGetDTO
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    //public bool IsActive { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? StudentName { get; set; }
    public string? TeacherName { get; set; }
    public string Image { get; set; }
    public List<string> Roles { get; set; }
    //public List<string> RoleNames { get; set; }
}