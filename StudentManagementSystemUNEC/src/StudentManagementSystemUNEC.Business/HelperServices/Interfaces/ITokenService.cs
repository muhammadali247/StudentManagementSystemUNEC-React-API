using StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(AppUser user, IList<string> roles, TimeSpan tokenLifetime);
    Task<RefreshToken> GenerateRefreshTokenAsync(AppUser user, TimeSpan tokenLifetime);
}