using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.LessonTypeDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.LessonTypeExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.TeacherRoleExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class LessonTypeService : ILessonTypeService
{
    private readonly ILessonTypeRepository _lessonTypeRepository;
    private readonly IMapper _mapper;

    public LessonTypeService(ILessonTypeRepository lessonTypeRepository, IMapper mapper = null)
    {
        _lessonTypeRepository = lessonTypeRepository;
        _mapper = mapper;
    }


    public async Task<List<LessonTypeGetDTO>> GetAllLessonTypesAsync(string? search)
    {
        try
        {
            //var lessonTypes = await _lessonTypeRepository.GetFiltered(t => search != null ? t.Name.Contains(search) : true).ToListAsync();
            var lessonTypes = await _lessonTypeRepository.GetAll(true).ToListAsync();
            var lessonTypeGetDTOs = _mapper.Map<List<LessonTypeGetDTO>>(lessonTypes);
            return lessonTypeGetDTOs;
        }
        catch (Exception)
        {

            throw;
        }
        //try
        //{
        //    if (_lessonTypeRepository == null)
        //    {
        //        // Handle the situation where dependencies are not properly initialized.
        //        // You can throw an exception, log an error, or take appropriate action.
        //        throw new InvalidOperationException("Dependencies not properly initialized.");
        //    }

        //    if (_mapper == null)
        //    {
        //        // Handle the situation where dependencies are not properly initialized.
        //        // You can throw an exception, log an error, or take appropriate action.
        //        throw new InvalidOperationException("Dependencies not properly initialized.");
        //    }

        //    var lessonTypes = await _lessonTypeRepository.GetAll(true)?.ToListAsync();

        //    if (lessonTypes == null)
        //    {
        //        // Handle the situation where the result is null.
        //        // You can throw an exception, log an error, or take appropriate action.
        //        throw new InvalidOperationException("Lesson types not retrieved.");
        //    }

        //    var lessonTypeGetDTOs = _mapper.Map<List<LessonTypeGetDTO>>(lessonTypes);
        //    return lessonTypeGetDTOs;
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }

    public async Task<LessonTypeGetDTO> GetLessonTypeByIdAsync(Guid Id)
    {
        var lessonType = await _lessonTypeRepository.GetSingleAsync(lt => lt.Id == Id);

        if (lessonType == null)
            throw new LessonTypeNotFoundByIdException($"Lesson type not found by id: {Id}");

        var lessonTypeGetDTO = _mapper.Map<LessonTypeGetDTO>(lessonType);
        return lessonTypeGetDTO;
    }

    public async Task CreateLessonTypeAsync(LessonTypePostDTO lessonTypePostDTO)
    {
        var lessonType = _mapper.Map<LessonType>(lessonTypePostDTO);
        await _lessonTypeRepository.CreateAsync(lessonType);
        await _lessonTypeRepository.SaveAsync();
    }

    public async Task UpdateLessonTypeAsync(Guid Id, LessonTypePutDTO lessonTypePutDTO)
    {
        var lessonType = await _lessonTypeRepository.GetSingleAsync(tr => tr.Id == Id);

        if (lessonType is null)
            throw new TeacherRoleNotFoundByIdException($"Teacher role not found by id: {Id}");

        lessonType = _mapper.Map(lessonTypePutDTO, lessonType);
        _lessonTypeRepository.Update(lessonType);
        await _lessonTypeRepository.SaveAsync();
    }

    public async Task DeleteLessonTypeAsync(Guid Id)
    {
        var lessonType = await _lessonTypeRepository.GetSingleAsync(tr => tr.Id == Id);

        if (lessonType is null)
            throw new TeacherRoleNotFoundByIdException($"Teacher role not found by id: {Id}");

        _lessonTypeRepository.Delete(lessonType);
        await _lessonTypeRepository.SaveAsync();
    }

    public async Task<bool> CheckLessonTypeExistsByIdAsync(Guid Id)
    {
        return await _lessonTypeRepository.isExsistAsync(tr => tr.Id == Id);
    }
}