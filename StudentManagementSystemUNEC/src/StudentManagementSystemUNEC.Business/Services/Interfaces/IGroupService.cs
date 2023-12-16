using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IGroupService
{
    public Task<List<GroupGetDTO>> GetAllGroupsAsync(string? search);
    public Task<GroupGetDTO> GetGroupByIdAsync(Guid Id);
    public Task CreateGroupAsync(GroupPostDTO groupPostDTO);
    public Task DeleteStudentAsync(Guid Id);
    public Task UpdateStudentAsync(Guid Id, GroupPutDTO groupPutDTO);
    public Task<bool> CheckStudentExistsByIdAsync(Guid Id);
}
