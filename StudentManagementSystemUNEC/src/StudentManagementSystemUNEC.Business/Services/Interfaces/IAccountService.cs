using StudentManagementSystemUNEC.Business.DTOs.AccDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IAccountService
{
    Task<string> RegisterAsync(RegisterDTO registerDTO);
}