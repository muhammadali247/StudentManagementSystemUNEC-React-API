using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.CommonDTOs;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using System.Net;

namespace StudentManagementSystemUNEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AccountsController(AppDbContext context, IMapper mapper, IUserService userService)
    {
        _userService = userService;
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUserUser(UserPostDTO userPostDTO)
    {
        var userCreateResponse = await _userService.CreateAccountAsync(userPostDTO);
        return StatusCode((int)HttpStatusCode.Created, userCreateResponse);
    }

    [HttpPost("create-student-user")]
    public async Task<IActionResult> CreateStudentUser(studentUserPostDTO studentUserPostDTO)
    {
        var userCreateResponse = await _userService.CreateStudentAccountAsync(studentUserPostDTO);
        return StatusCode((int)HttpStatusCode.Created, userCreateResponse);
    }

    [HttpPost("resend-otp/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResendOTP(string userId)
    {
        var resendOTPResponse = await _userService.ResendOTP(userId);
        return StatusCode((int)HttpStatusCode.OK, resendOTPResponse);
    }

    [HttpPost("verify-account/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyAccount(string userId, VerifyAccountDTO verifyAccountDTO)
    {
        var userVerifyResponse = await _userService.VerifyAccount(userId, verifyAccountDTO);
        return StatusCode((int)HttpStatusCode.OK, userVerifyResponse);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Student,Teacher")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsersAsync();
        return Ok(user);
    }

    [HttpGet("unassigned-users")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Student,Teacher")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetUnassignedUsers()
    {
        var user = await _userService.GetAllUnassignedUsersAsync();
        return Ok(user);
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Student,Teacher")]
    public async Task<IActionResult> GetUserDetails(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UdpateUser(string id, UserPutDTO userPutDTO)
    {
        await _userService.UpdateUserAsync(id, userPutDTO);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "user updated successefully"));
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "User Deleted successefully"));
    }
}