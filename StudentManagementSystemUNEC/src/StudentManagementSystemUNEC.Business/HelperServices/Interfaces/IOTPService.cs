using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

public interface IOTPService
{
    public string GenerateOTP();
    void SetOTPForUser(AppUser user);
}