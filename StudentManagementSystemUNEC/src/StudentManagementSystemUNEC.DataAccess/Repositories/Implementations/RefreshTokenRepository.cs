using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
    }

   public async Task<RefreshToken> FindNonRevokedTokenByUserIdAsync(string userId)
    {
        var refreshToken = await _context.refreshTokens.FirstOrDefaultAsync(t => t.UserId == userId && !t.IsRevoked);
        return refreshToken;
    }

    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        var refreshToken = await _context.refreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        return refreshToken;
    }
}