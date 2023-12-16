using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface IGroupSubjectService
{
    Task<List<GroupSubjectGetDTO>> GetAllGroupSubjectsAsync();
    Task<GroupSubjectGetDTO> GetGroupSubjectByIdAsync(Guid id);
    Task CreateGroupSubjectAsync(GroupSubjectPostDTO groupSubjectPostDTO);
    Task UpdateGroupSubjectAsync(Guid id, GroupSubjectPutDTO groupSubjectPutDTO);
    Task DeleteGroupSubjectAsync(Guid id);
    public Task<bool> CheckGroupSubjectExistsByIdAsync(Guid Id);
}
