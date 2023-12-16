using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.ExamExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.ExamTypeExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.GroupSubjectExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IExamTypeRepository _examTypeRepository;
    private readonly IGroupSubjectRepository _groupSubjectRepository;
    private readonly IMapper _mapper;

    public ExamService(IExamRepository examRepository, IMapper mapper, IGroupSubjectRepository groupSubjectRepository, IExamTypeRepository examTypeRepository)
    {
        _examRepository = examRepository;
        _mapper = mapper;
        _groupSubjectRepository = groupSubjectRepository;
        _examTypeRepository = examTypeRepository;
    }

    public async Task<List<ExamGetDTO>> GetAllExamsAsync(string? search)
    {
        var exams = await _examRepository.GetFiltered(e => search != null ? e.Name.Contains(search) : true, isTracking: true, "ExamType", "GroupSubject.Group", "GroupSubject.Subject", "ExamResults.Student").ToListAsync();

        var examGetDTOs = _mapper.Map<List<ExamGetDTO>>(exams);

        return examGetDTOs;
    }

    public async Task<ExamGetDTO> GetExamByIdAsync(Guid id)
    {
        var exam = await _examRepository.GetSingleAsync(e => e.Id == id, isTracking: true, "ExamType", "GroupSubject.Group", "GroupSubject.Subject", "ExamResults.Student");

        if (exam is null)
            throw new ExamNotFoundByIdException($"Exam not found by id:{id}");

        var examGetDTO = _mapper.Map<ExamGetDTO>(exam);

        return examGetDTO;
    }

    public async Task CreateExamAsync(ExamPostDTO examPostDTO)
    {
        try
        {
            if (!await _examTypeRepository.isExsistAsync(et => et.Id == examPostDTO.ExamTypeId))
                throw new ExamTypeNotFoundByIdException("Exam's type not found");

            if (!await _groupSubjectRepository.isExsistAsync(gs => gs.Id == examPostDTO.GroupSubjectId))
                throw new GroupSubjectNotFoundByIdException("Group subject not found");

            var newExam = _mapper.Map<Exam>(examPostDTO);

            await _examRepository.CreateAsync(newExam);
            await _examRepository.SaveAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateExamAsync(Guid id, ExamPutDTO examPutDTO)
    {
        var exam = await _examRepository.GetSingleAsync(e => e.Id == id, isTracking: true, "ExamType", "GroupSubject.Group", "GroupSubject.Subject");

        if (exam is null)
            throw new ExamNotFoundByIdException($"Exam not found by id:{id}");

        if (examPutDTO.ExamTypeId != null && exam.ExamTypeId != examPutDTO.ExamTypeId)
        {
            if (!await _examTypeRepository.isExsistAsync(et => et.Id == examPutDTO.ExamTypeId))
                throw new ExamTypeNotFoundByIdException("Exam type not found");
        }

        if (examPutDTO.GroupSubjectId != null && exam.GroupSubjectId != examPutDTO.GroupSubjectId)
        {
            if (!await _groupSubjectRepository.isExsistAsync(gs => gs.Id == examPutDTO.GroupSubjectId))
                throw new GroupSubjectNotFoundByIdException("Group subject not found");
        }

        exam = _mapper.Map(examPutDTO, exam);
        _examRepository.Update(exam);
        await _examRepository.SaveAsync();
    }

    public async Task DeleteExamAsync(Guid id)
    {
        var exam = await _examRepository.GetSingleAsync(e => e.Id == id, isTracking: true, "ExamType", "GroupSubject.Group", "GroupSubject.Subject");

        if (exam is null)
            throw new ExamNotFoundByIdException($"Exam not found by id:{id}");

        _examRepository.Delete(exam);
        await _examRepository.SaveAsync();
    }
}