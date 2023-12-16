using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Core.Entities;

public class RefreshToken : BaseSectionEntity
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public bool IsRevoked { get; set; } = false;
}