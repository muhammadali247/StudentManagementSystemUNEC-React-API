using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken> FindNonRevokedTokenByUserIdAsync(string userId);
    Task<RefreshToken> GetByTokenAsync(string token);
}