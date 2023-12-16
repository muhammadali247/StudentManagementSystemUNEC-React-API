using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

public class GroupPostDTO
{
    public string Name { get; set; }
    public SubGroup? subGroup { get; set; }
    public DateTime CreationYear { get; set; }
    public Guid? FacultyId { get; set; }
    public ICollection<Guid>? StudentIds { get; set; }
}
