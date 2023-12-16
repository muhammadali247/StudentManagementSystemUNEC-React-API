using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemUNEC.Business.DTOs.AccDTOs;

public class RegisterDTO
{
    public string UserName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? TeacherId { get; set; }
    public List<string> RoleId { get; set; }
}   