using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;
using StudentManagementSystemUNEC.Business.Exceptions.TeacherRoleExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class TeacherRoleService : ITeacherRoleService
{
    private readonly ITeacherRoleRepository _teacherRoleRepository;
    private readonly IMapper _mapper;

    public TeacherRoleService(ITeacherRoleRepository teacherRoleRepository, IMapper mapper)
    {
        _teacherRoleRepository = teacherRoleRepository;
        _mapper = mapper;
    }

    public async Task<List<TeacherRoleGetDTO>> GetAllTeacherRolesAsync(string? search)
    {
        //var teacherRoles = await _teacherRoleRepository.GetFiltered(t => search != null ? t.Name.Contains(search) : true).ToListAsync();
        var teacherRoles = await _teacherRoleRepository.GetAll().ToListAsync();
        var teacherRoleGetDTOs = _mapper.Map<List<TeacherRoleGetDTO>>(teacherRoles);
        return teacherRoleGetDTOs;
    }

    public async Task<TeacherRoleGetDTO> GetTeacherRoleByIdAsync(Guid Id)
    {
        var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr => tr.Id == Id);

        if (teacherRole is null)
            throw new TeacherRoleNotFoundByIdException($"Teacher role not found by id: {Id}");

        var teacherRoleGetDTO = _mapper.Map<TeacherRoleGetDTO>(teacherRole);
        return teacherRoleGetDTO;
    }

    public async Task CreateTeacherRoleAsync(TeacherRolePostDTO teacherRolePostDTO)
    {
        var teacherRole = _mapper.Map<TeacherRole>(teacherRolePostDTO);
        await _teacherRoleRepository.CreateAsync(teacherRole);
        await _teacherRoleRepository.SaveAsync();
    }

    public async Task UpdateTeacherRoleAsync(Guid Id, TeacherRolePutDTO teacherRolePutDTO)
    {
        var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr => tr.Id == Id);

        if (teacherRole is null)
            throw new TeacherRoleNotFoundByIdException($"Teacher role not found by id: {Id}");

        teacherRole = _mapper.Map(teacherRolePutDTO, teacherRole);
        _teacherRoleRepository.Update(teacherRole);
        await _teacherRoleRepository.SaveAsync();
    }

    public async Task DeleteTeacherRoleAsync(Guid Id)
    {
        var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr => tr.Id == Id);

        if (teacherRole is null)
            throw new TeacherRoleNotFoundByIdException($"Teacher role not found by id: {Id}");

        _teacherRoleRepository.Delete(teacherRole);
        await _teacherRoleRepository.SaveAsync();
    }

    public async Task<bool> CheckTeacherRoleExistsByIdAsync(Guid Id)
    {
        return await _teacherRoleRepository.isExsistAsync(tr => tr.Id == Id);
    }
}