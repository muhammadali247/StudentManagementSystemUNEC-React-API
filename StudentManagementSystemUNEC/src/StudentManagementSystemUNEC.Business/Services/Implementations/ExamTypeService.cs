using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.ExamTypeExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class ExamTypeService : IExamTypeService
{
    private readonly IExamTypeRepository _examTypeRepository;
    private readonly IMapper _mapper;

    public ExamTypeService(IExamTypeRepository examTypeRepository, IMapper mapper)
    {
        _examTypeRepository = examTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<ExamTypeGetDTO>> GetAllExamTypesAsync(string? search)
    {
        try
        {
            //var examTypes = await _examTypeRepository.GetFiltered(t => search != null ? t.Name.Contains(search) : true).ToListAsync();
            var examTypes = await _examTypeRepository.GetAll().ToListAsync();
            var examTypeGetDTOs = _mapper.Map<List<ExamTypeGetDTO>>(examTypes);
            return examTypeGetDTOs;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ExamTypeGetDTO> GetExamTypeByIdAsync(Guid Id)
    {
        var examType = await _examTypeRepository.GetSingleAsync(tr => tr.Id == Id);

        if (examType is null)
            throw new ExamTypeNotFoundByIdException($"Exam type not found by id: {Id}");

        var examTypeGetDTO = _mapper.Map<ExamTypeGetDTO>(examType);
        return examTypeGetDTO;
    }

    public async Task CreateExamTypeAsync(ExamTypePostDTO examTypePostDTO)
    {
        var examType = _mapper.Map<ExamType>(examTypePostDTO);
        await _examTypeRepository.CreateAsync(examType);
        await _examTypeRepository.SaveAsync();
    }

    public async Task UpdateExamTypeAsync(Guid Id, ExamTypePutDTO examTypePutDTO)
    {
        var examType = await _examTypeRepository.GetSingleAsync(tr => tr.Id == Id);

        if (examType is null)
            throw new ExamTypeNotFoundByIdException($"Exam type not found by id: {Id}");

        examType = _mapper.Map(examTypePutDTO, examType);
        _examTypeRepository.Update(examType);
        await _examTypeRepository.SaveAsync();
    }

    public async Task DeleteExamTypeAsync(Guid Id)
    {
        var examType = await _examTypeRepository.GetSingleAsync(tr => tr.Id == Id);

        if (examType is null)
            throw new ExamTypeNotFoundByIdException($"Exam type not found by id: {Id}");

        _examTypeRepository.Delete(examType);
        await _examTypeRepository.SaveAsync();
    }

    public async Task<bool> CheckExamTypeExistsByIdAsync(Guid Id)
    {
        return await _examTypeRepository.isExsistAsync(tr => tr.Id == Id);
    }
}