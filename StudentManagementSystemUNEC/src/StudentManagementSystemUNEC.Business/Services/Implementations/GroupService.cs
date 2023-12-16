using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.GroupExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }


    public async Task<List<GroupGetDTO>> GetAllGroupsAsync(string? search)
    {
        try
        {
            var groups = await _groupRepository.GetFiltered(g => search != null ? g.Name.Contains(search) : true, isTracking: true, "Faculty", "StudentGroups.Student", "GroupSubjects.Subject", "GroupSubjects.teacherSubjects.Teacher", "GroupSubjects.teacherSubjects.TeacherRole").ToListAsync();

            var groupGetDTOs = _mapper.Map<List<GroupGetDTO>>(groups);

            return groupGetDTOs;
        }
        catch (Exception)
        {
            throw new Exception("error occured");
        }
    }

    public async Task<GroupGetDTO> GetGroupByIdAsync(Guid Id)
    {
        try
        {
            var group = await _groupRepository.GetSingleAsync(g => g.Id == Id, isTracking: true, "Faculty", "StudentGroups.Student", "GroupSubjects.Subject", "GroupSubjects.teacherSubjects.Teacher", "GroupSubjects.teacherSubjects.TeacherRole");

            if (group is null)
                throw new GroupNotFoundByIdException($"Group not found by id: {Id}");

            var groupGetDTO = _mapper.Map<GroupGetDTO>(group);
            return groupGetDTO;
        }
        catch (Exception)
        {
            throw new Exception("error occured");
            //Console.WriteLine(ex.Message);
        }
    }

    public async Task CreateGroupAsync(GroupPostDTO groupPostDTO)
    {
        try
        {
            if (groupPostDTO is null)
            {
                throw new ArgumentNullException(nameof(groupPostDTO), "groupPostDTO is null");
            }

            var group = _mapper.Map<Group>(groupPostDTO);

            // Check for null references in the student object properties
            if (group is null)
            {
                throw new Exception("Mapped group object is null");
            }

            await _groupRepository.CreateAsync(group);
            await _groupRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task UpdateStudentAsync(Guid Id, GroupPutDTO groupPutDTO)
    {
        var group = await _groupRepository.GetSingleAsync(g => g.Id == Id);

        if (group is null)
            throw new GroupNotFoundByIdException($"Group not found by id: {Id}");

        group = _mapper.Map(groupPutDTO, group);

        _groupRepository.Update(group);
        await _groupRepository.SaveAsync();
    }

    public async Task DeleteStudentAsync(Guid Id)
    {
        var group = await _groupRepository.GetSingleAsync(g => g.Id == Id);

        if (group is null)
            throw new GroupNotFoundByIdException($"Group not found by id: {Id}");

        _groupRepository.Delete(group);
        await _groupRepository.SaveAsync();
    }

    public async Task<bool> CheckStudentExistsByIdAsync(Guid Id)
    {
        return await _groupRepository.isExsistAsync(g => g.Id == Id);
    }
}
