using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.Core.Entities.Identity;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementSystemUNEC.Business.HelperServices.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenService(IConfiguration config, IRefreshTokenRepository refreshTokenRepository)
    {
        _config = config;
        _refreshTokenRepository = refreshTokenRepository;
    }


    public async Task<RefreshToken> GenerateRefreshTokenAsync(AppUser user, TimeSpan tokenLifetime)
    {
        // Find an existing non-revoked refresh token for the user
        var existingRefreshToken = await _refreshTokenRepository.FindNonRevokedTokenByUserIdAsync(user.Id);

        if (existingRefreshToken != null)
        {
            // Set the existing token as revoked
            existingRefreshToken.IsRevoked = true;

                // Update the existing token in the database
                //await _refreshTokenRepository.Update(existingRefreshToken.Id, existingRefreshToken);

            // Update the existing token in the database
            _refreshTokenRepository.Update(existingRefreshToken);
        }

        // Generate a new refresh token string (e.g., a random string)
        string refreshTokenString = GenerateRefreshTokenString();

        // Create a new RefreshToken entity
        var refreshToken = new RefreshToken
        {
            Token = refreshTokenString,
            Expires = DateTime.Now.Add(tokenLifetime),
            UserId = user.Id
        };

        // Store the new refresh token in the database
        await _refreshTokenRepository.CreateAsync(refreshToken);
        await _refreshTokenRepository.SaveAsync();

        return refreshToken;
    }

    public async Task<string> GenerateTokenAsync(AppUser user, IList<string> roles, TimeSpan tokenLifetime)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        // Add roles to claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.Add(tokenLifetime),
            notBefore:DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        //return new TokenResponseDTO(token, jwtSecurityToken.ValidTo);
        return token;
    }

    private string GenerateRefreshTokenString(int length = 64)
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] tokenChars = new char[length];
        byte[] randomBytes = new byte[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        for (int i = 0; i < length; i++)
        {
            int index = randomBytes[i] % allowedChars.Length;
            tokenChars[i] = allowedChars[index];
        }

        return new string(tokenChars);
    }
}