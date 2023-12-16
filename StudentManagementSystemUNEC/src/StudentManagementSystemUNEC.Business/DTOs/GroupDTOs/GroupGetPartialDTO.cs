namespace StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

public class GroupGetPartialDTO
{
    public Guid Id { get; set; }
    public byte StudentCount { get; set; }
    public string Name { get; set; }
    public DateTime CreationYear { get; set; }
}
