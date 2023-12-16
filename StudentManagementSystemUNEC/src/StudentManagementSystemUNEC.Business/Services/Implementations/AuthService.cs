using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.UserExceptions;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Business.Validators.AccountValidators;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;
using static StudentManagementSystemUNEC.Business.Services.Interfaces.IAuthService;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IValidator<LoginDTO> _loginValidator;
    private readonly IValidator<ResetPasswordDTO> _resetPasswordValidator;
    private readonly IValidator<ForgotPasswordDTO> _forgotPasswordValidator;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IFileReadService _fileReadService;
    private readonly IValidator<RenewTokenDTO> _renewTokenValidator;

    public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IValidator<LoginDTO> loginValidator, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IValidator<ForgotPasswordDTO> forgotPasswordValidator, IConfiguration configuration, IFileReadService fileReadService, IWebHostEnvironment webHostEnvironment, IEmailService emailService, IValidator<ResetPasswordDTO> resetPasswordValidator, IValidator<RenewTokenDTO> renewTokenValidator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _loginValidator = loginValidator;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _forgotPasswordValidator = forgotPasswordValidator;
        _configuration = configuration;
        _fileReadService = fileReadService;
        _webHostEnvironment = webHostEnvironment;
        _emailService = emailService;
        _resetPasswordValidator = resetPasswordValidator;
        _renewTokenValidator = renewTokenValidator;
    }


    public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginDTO);
        if (!validationResult.IsValid) throw new LoginFailException("Validation error(s) reported");

        AppUser user = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail)
                        ?? await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);

        if (user is null) throw new LoginFailException("Username/Email or password is incorrect!");

        var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var refreshTokenLifetime = loginDTO.RememberMe ? TimeSpan.FromDays(30) : TimeSpan.FromMinutes(42);
            var accessTokenLifetime = TimeSpan.FromMinutes(5);

            // Generate the refresh token
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user, refreshTokenLifetime);

            // Generate the access token
            var accessToken = await _tokenService.GenerateTokenAsync(user, roles, accessTokenLifetime);

            return new LoginResponse(accessToken, refreshToken);
        }
        else
        {
            var failureMessage = result.IsLockedOut ? "Your account is locked!" : "Invalid username or password!";
            throw new UserLockOutException("User lockout reported");
        }
    }

    public async Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
    {
        var validationResult = await _forgotPasswordValidator.ValidateAsync(forgotPasswordDTO);
            if (!validationResult.IsValid) throw new ForgotPasswordFailException("Validation error(s) reported");

        var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
        if (user is null) throw new UserNotFoundByIdException("No user associated with email address exists!");

        // Generate reset token
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);


        // Generate Email using HTML Template
        string resetLink = $"{_configuration["FrontEndUrl"]}/auth/resetPassword?email={user.Email}&token={Uri.EscapeDataString(resetToken)}";
        //string path = _configuration.GetValue<string>("ForgotPasswordTemplatePath");
        string emailBody = string.Empty;


        //// Email logic to reset password
        emailBody = await GetForgotPasswordTemplateAsync(resetLink);
        string subject = "Password Reset Instructions";

        _emailService.Send(user.Email, subject, emailBody);

        return "Reset password link has been sent to your email.";
    }

    public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
    {
        var validationResult = await _resetPasswordValidator.ValidateAsync(resetPasswordDTO);
            if (!validationResult.IsValid) throw new ResetPasswordFailException("Validation error(s) reported");

        var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user is null) throw new UserNotFoundByIdException("No user associated with email address exists!");

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

        if (!result.Succeeded)
        {
            throw new UserResetPasswordFailException(result.Errors);
        }

        return "Password has been reset successfully.";
    }

    public async Task<string> ValidateResetPassword(string Email, string Token)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user is null)
            throw new UserNotFoundByIdException("User not found");

        var decodedToken = Uri.UnescapeDataString(Token);
        var isValid = await _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", decodedToken);

        if (!isValid)
            throw new TokenNotValidException("Invalid token");

        return "Token is valid";
    }

    public async Task<string> RenewToken(RenewTokenDTO renewTokenDTO)
    {
        var validationResult = await _renewTokenValidator.ValidateAsync(renewTokenDTO);
            if (!validationResult.IsValid) throw new ResetPasswordFailException("Validation error(s) reported");

        // Validate and retrieve the refresh token
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(renewTokenDTO.Token);

        if (refreshToken == null || refreshToken.Expires < DateTime.Now || refreshToken.IsRevoked)
        {
            // Refresh token is invalid, expired, or revoked
            throw new InvalidRefreshTokenException("Invalid refresh token.");
        }

        // Retrieve the user associated with the refresh token
        var user = await _userManager.FindByIdAsync(refreshToken.UserId);

        if (user == null)
            // User not found
            throw new UserNotFoundByIdException("User not found");

        // Generate a new access token
        var roles = await _userManager.GetRolesAsync(user);
        var accessTokenLifetime = TimeSpan.FromMinutes(5);
        var accessToken = await _tokenService.GenerateTokenAsync(user, roles, accessTokenLifetime);

        //return accessToken.Token;
        return accessToken;
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        if (userId is null)
            throw new UserIdIsRequiredException("User Id is required!");

        var existingRefreshToken = await _refreshTokenRepository.FindNonRevokedTokenByUserIdAsync(userId);

        if (existingRefreshToken != null)
        {
            // Set the existing token as revoked
            existingRefreshToken.IsRevoked = true;

            // Update the existing token in the database
            _refreshTokenRepository.Update(existingRefreshToken);
        }

        return true;
    }

    private async Task<string> GetForgotPasswordTemplateAsync(string link)
    {
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "reset-password.html");

        string body = string.Empty;
        body = _fileReadService.ReadFile(path, body);

        body = body.Replace("{{link}}", link);
        return body;
    }
}