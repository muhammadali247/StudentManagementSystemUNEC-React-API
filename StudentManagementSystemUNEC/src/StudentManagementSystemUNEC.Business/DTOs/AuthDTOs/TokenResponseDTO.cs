namespace StudentManagementSystemUNEC.Business.DTOs.AuthDTOs;

public class TokenResponseDTO
{
    public string Token { get; set; }
    public DateTime _expireDate { get; set; }
    public TokenResponseDTO(string token, DateTime expireDate)
    {
        Token = token;
        _expireDate = expireDate;
    }
}