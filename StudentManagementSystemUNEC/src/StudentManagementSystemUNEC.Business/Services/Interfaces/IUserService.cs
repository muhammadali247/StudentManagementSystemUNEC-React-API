using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateAccountAsync(UserPostDTO userPostDTO);
    Task<string> CreateStudentAccountAsync(studentUserPostDTO studentUserPostDTO);
    Task<string> ResendOTP(string userId);
    Task<string> VerifyAccount(string userId, VerifyAccountDTO verifyAccountDTO);


    Task<List<UserGetDTO>> GetAllUsersAsync();
    Task<List<UnassignedUserGetDTO>> GetAllUnassignedUsersAsync();
    Task<UserGetDTO> GetUserByIdAsync(string Id);
    Task<UserPutDTO> GetUserByIdForUpdateAsync(string Id);
    Task UpdateUserAsync(string id, UserPutDTO userPutDTO);
    Task DeleteUserAsync(string id);
}