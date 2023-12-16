using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

public class GroupPutDTO
{
    public string Name { get; set; }
    public SubGroup? subGroup { get; set; }
    public DateTime CreationYear { get; set; }
    public Guid? FacultyId { get; set; }
    public List<Guid>? StudentIds { get; set; }
}
