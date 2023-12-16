using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Business.HelperServices.Implementations;

public class OTPService : IOTPService
{
    public string GenerateOTP()
    {
        Random random = new Random();
        int otpNumber = random.Next(100000, 1000000);
        return otpNumber.ToString();
    }

    public void SetOTPForUser(AppUser user)
    {
        user.OTP = GenerateOTP();
        user.OTPExpiryDate = DateTime.UtcNow.AddMinutes(5);  // OTP expires in 5 minutes
    }
}