using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.GroupSubjectExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class GroupSubjectService : IGroupSubjectService
{
    private readonly AppDbContext _context;
    private readonly IGroupSubjectRepository _groupSubjectRepository;
    private readonly ITeacherSubjectService _teacherSubjectService;
    private readonly IMapper _mapper;

    public GroupSubjectService(IGroupSubjectRepository groupSubjectRepository, IMapper mapper, AppDbContext context, ITeacherSubjectService teacherSubjectService)
    {
        _groupSubjectRepository = groupSubjectRepository;
        _mapper = mapper;
        _context = context;
        _teacherSubjectService = teacherSubjectService;
    }

    public async Task<List<GroupSubjectGetDTO>> GetAllGroupSubjectsAsync()
    {
        try
        {
            var groupSubjects = await _groupSubjectRepository.GetAll(isTracking: true, "teacherSubjects.Teacher", "teacherSubjects.Teacher.teacherSubjects.TeacherRole", "Group.Faculty", "Subject", "Group.StudentGroups.Student").ToListAsync();

            var groupSubjectGetDTOs = _mapper.Map<List<GroupSubjectGetDTO>>(groupSubjects);

            return groupSubjectGetDTOs;
        }
        catch (Exception)
        {
            throw new Exception("Error occured");
        }
    }

    public async Task<GroupSubjectGetDTO> GetGroupSubjectByIdAsync(Guid id)
    {
        var groupSubjects = await _groupSubjectRepository.GetSingleAsync(g => g.Id == id, isTracking: true, "teacherSubjects.Teacher.teacherSubjects.TeacherRole", "Group.Faculty", "Subject");

        var groupSubjectGetDTO = _mapper.Map<GroupSubjectGetDTO>(groupSubjects);

        return groupSubjectGetDTO;
    }

    public async Task CreateGroupSubjectAsync(GroupSubjectPostDTO groupSubjectPostDTO)
    {
        var newGroupSubject = _mapper.Map<GroupSubject>(groupSubjectPostDTO);
        await _groupSubjectRepository.CreateAsync(newGroupSubject);
        await _groupSubjectRepository.SaveAsync();


        if (groupSubjectPostDTO.teacherRole is not null)
        {
            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
            foreach (var teacherRole in groupSubjectPostDTO.teacherRole)
            {
                var teacherSubject = new TeacherSubject()
                {
                    TeacherId = teacherRole.TeacherId,
                    GroupSubjectId = newGroupSubject.SubjectId,
                    TeacherRoleId = teacherRole.RoleId

                };
                teacherSubjects.Add(teacherSubject);
            }
            await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);
            newGroupSubject.teacherSubjects = teacherSubjects;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateGroupSubjectAsync(Guid id, GroupSubjectPutDTO groupSubjectPutDTO)
    {
        var existingGroupSubject = await _groupSubjectRepository.GetSingleAsync(gs => gs.Id == id, isTracking: true, "teacherSubjects");

        if (existingGroupSubject is null)
            throw new GroupSubjectNotFoundByIdException("Group's subject not found");


        existingGroupSubject = _mapper.Map(groupSubjectPutDTO, existingGroupSubject);
        _groupSubjectRepository.Update(existingGroupSubject);
        await _groupSubjectRepository.SaveAsync();


        if (groupSubjectPutDTO.teacherRole is not null)
        {


            List<TeacherSubject>? teachersToRemove = existingGroupSubject.teacherSubjects?.Where(ts => !groupSubjectPutDTO.teacherRole.Any(tr => tr.TeacherId == ts.TeacherId && tr.RoleId == ts.TeacherRoleId)).ToList();
            var teachersRoleAdd = groupSubjectPutDTO.teacherRole.Where(tr => !existingGroupSubject.teacherSubjects.Any(ts => ts.TeacherId == tr.TeacherId && ts.TeacherRoleId == tr.RoleId)).ToList();

            if (teachersToRemove is not null)
            {
                await _teacherSubjectService.DeleteTeacherSubjectsAsync(teachersToRemove);
            }

            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
            foreach (var teacherRole in teachersRoleAdd)
            {
                var teacherSubject = new TeacherSubject()
                {
                    TeacherId = teacherRole.TeacherId,
                    GroupSubjectId = existingGroupSubject.SubjectId,
                    TeacherRoleId = teacherRole.RoleId

                };
                teacherSubjects.Add(teacherSubject);
            }
            await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);
            existingGroupSubject.teacherSubjects?.AddRange(teacherSubjects);
            await _context.SaveChangesAsync();

        }
        else
        {
            if (existingGroupSubject.teacherSubjects != null)
            {
                List<TeacherSubject> teachersToRemove = existingGroupSubject.teacherSubjects.ToList();
                await _teacherSubjectService.DeleteTeacherSubjectsAsync(teachersToRemove);
                existingGroupSubject.teacherSubjects = null;
                await _context.SaveChangesAsync();

            }
        }
    }

    public async Task DeleteGroupSubjectAsync(Guid id)
    {
        var existingGroupSubject = await _groupSubjectRepository.GetSingleAsync(gs => gs.Id == id);

        if (existingGroupSubject is null)
            throw new GroupSubjectNotFoundByIdException("Group's subject not found");

        if (existingGroupSubject.teacherSubjects is not null)
        {
            var existingTeacherSubjects = await _teacherSubjectService.GetTeacherSubjectsForGroupSubjectAsync(existingGroupSubject.Id);
            await _teacherSubjectService.DeleteTeacherSubjectsAsync(existingTeacherSubjects);
        }

        _groupSubjectRepository.Delete(existingGroupSubject);
        await _groupSubjectRepository.SaveAsync();
    }

    public async Task<bool> CheckGroupSubjectExistsByIdAsync(Guid Id)
    {
        return await _groupSubjectRepository.isExsistAsync(gs => gs.Id == Id);
    }
}