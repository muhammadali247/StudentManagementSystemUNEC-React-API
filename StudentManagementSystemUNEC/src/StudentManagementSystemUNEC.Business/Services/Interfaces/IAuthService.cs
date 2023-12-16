using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;
public interface IAuthService
{
    //public record LoginResponse(TokenResponseDTO AccessToken, RefreshToken RefreshToken);
    public record LoginResponse(string AccessToken, RefreshToken RefreshToken);
    Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
    Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
    Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);
    Task<string> ValidateResetPassword(string Email, string Token);
    Task<string> RenewToken(RenewTokenDTO renewTokenDTO);
    Task<bool> LogoutAsync(string userId);
}