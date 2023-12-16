using Microsoft.AspNetCore.Identity;

namespace StudentManagementSystemUNEC.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public bool IsActive { get; set; }
    public Student? Student { get; set; }
    //public string? StudentId { get; set; }
    public Teacher? Teacher { get; set; }
    //public string? TeacherId { get; set; }
    public string? OTP { get; set; }
    public DateTime? OTPExpiryDate { get; set; }
}