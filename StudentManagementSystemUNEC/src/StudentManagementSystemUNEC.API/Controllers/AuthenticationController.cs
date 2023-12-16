using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.AccExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.AuthExceptions;
using StudentManagementSystemUNEC.Business.Services.Implementations;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [Route("Login")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var loginResponse = await _authService.LoginAsync(loginDTO);

        //// Set the access token cookie
        //SetAccessTokenCookie(loginResponse.AccessToken.Token);
        SetAccessTokenCookie(loginResponse.AccessToken);
        SetRefreshTokenCookie(loginResponse.RefreshToken);

        return Ok(new
        {
            AccessToken = loginResponse.AccessToken,
            RefreshToken = loginResponse.RefreshToken.Token
        });
    }

    [Route("forgot-password")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
    {
        var forgotPasswordResponse = await _authService.ForgotPassword(forgotPasswordDTO);
        return StatusCode((int)HttpStatusCode.OK, forgotPasswordResponse);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
    {
        var resetPasswordResponse = await _authService.ResetPassword(resetPasswordDTO);
        return StatusCode((int)HttpStatusCode.OK, resetPasswordResponse);
    }

    [HttpGet("validate-reset-password-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateResetPasswordToken(string email, string token)
    {
        var validateResetPasswordResponse = await _authService.ValidateResetPassword(email, token);
        return StatusCode((int)HttpStatusCode.OK, validateResetPasswordResponse);
    }

    [Route("renew-token")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RenewToken()
    {
        // Retrieve the refreshToken from HttpContext.Items
        if (!HttpContext.Items.TryGetValue("refreshToken", out var refreshTokenObject) || !(refreshTokenObject is string refreshToken))
        {
            // Handle the case where refreshToken is not found or is not a string
            return BadRequest(new { message = "Invalid refresh token." });
        }

        // Create the RenewTokenDTO using the captured refreshToken
        var renewTokenDTO = new RenewTokenDTO { Token = refreshToken };

        var renewTokenResponse = await _authService.RenewToken(renewTokenDTO);
        return StatusCode((int)HttpStatusCode.OK, renewTokenDTO);

        //// Update the HttpOnly access token cookie
        //SetAccessTokenCookie(response);

        //return Ok(new { AccessToken = response });
    }

    [HttpPost("logout")]
    //[Authorize] // Requires authentication
    
    public async Task<IActionResult> Logout()
    {
        // Retrieve the user's ID from the JWT claims
        //var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value; // initial version
        var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        var userId = userIdClaim?.Value;


        if (string.IsNullOrEmpty(userId))
        {
            // Handle the case where the user ID is not found
            throw new UserNotRegisteredException("User not authenticated");
        }

        // Send a command to handle the logout
        var logoutResponse = await _authService.LogoutAsync(userId);

        if (logoutResponse)
        {
            // Sign out the user
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Clear any authentication cookies, including access and refresh tokens
            //Response.Cookies.Delete("accessToken");
            Response.Cookies.Append(
                  "refreshToken",
                  "", // Empty value
                  new CookieOptions
                  {
                      HttpOnly = true,
                      Secure = true,
                      SameSite = SameSiteMode.None,
                      Expires = DateTimeOffset.UtcNow.AddMinutes(-5) // Set expiration to past time
                  }
              );

            // Return a successful logout response
            return Ok(new { message = "Successfully logged out" });
        }
        else
        {
            // Handle the error, such as token invalidation failure
            throw new LogoutFailException("Failed to log out");
        }

    }

    private void SetAccessTokenCookie(string accessToken)
    {
        // Decode the access token to get the expiration time
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken);
        var tokenExpirationTime = jwtToken.ValidTo;

        // Set the HttpOnly access token cookie
        Response.Cookies.Append(
            "accessToken",
            accessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = tokenExpirationTime
            }
        );
    }

    private void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        // Set the HttpOnly refresh token cookie
        Response.Cookies.Append(
            "refreshToken",
            refreshToken.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refreshToken.Expires
            }
        );
    }
}