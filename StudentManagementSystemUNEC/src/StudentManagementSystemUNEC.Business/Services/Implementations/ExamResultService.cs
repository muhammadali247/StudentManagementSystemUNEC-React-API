using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.ExamExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.ExamResultExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.StudentExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class ExamResultService : IExamResultService
{
    private readonly IExamResultRepository _examResultRepository;
    private readonly IMapper _mapper;
    private readonly IExamRepository _examRepository;
    private readonly IStudentRepository _studentRepository;

    public ExamResultService(IExamResultRepository examResultRepository, IMapper mapper, IExamRepository examRepository, IStudentRepository studentRepository)
    {
        _examRepository = examRepository;
        _studentRepository = studentRepository;
        _mapper = mapper;
        _examResultRepository = examResultRepository;
    }

    public async Task<List<ExamResultGetDTO>> GetAllExamResultsAsync(string? studentName)
    {
        var examResults = await _examResultRepository.GetFiltered(er => studentName != null ? ((er.Student.Name.Contains(studentName)) || (er.Student.Surname.Contains(studentName))) : true, isTracking: true,  "Student", "Exam.ExamType", "Exam.GroupSubject.Group", "Exam.GroupSubject.Subject").ToListAsync();
        var examResultGetDTOs = _mapper.Map<List<ExamResultGetDTO>>(examResults);
        return examResultGetDTOs;
    }

    public async Task<ExamResultGetDTO> GetExamResultByIdAsync(Guid id)
    {
        var examResult = await _examResultRepository.GetSingleAsync(er => er.Id == id, isTracking:true, "Student", "Exam.ExamType", "Exam.GroupSubject.Group", "Exam.GroupSubject.Subject");

        if (examResult is null)
            throw new ExamResultNotFoundByIdException($"Exam result not found by id:{id}");

        var examResultGetDTO = _mapper.Map<ExamResultGetDTO>(examResult);
        return examResultGetDTO;
    }

    public async Task CreateExamResultAsync(ExamResultPostDTO examResultPostDTO)
    {
        if (!await _examRepository.isExsistAsync(e => e.Id == examResultPostDTO.ExamId))
            throw new ExamNotFoundByIdException("Exam not found");

        if (!await _studentRepository.isExsistAsync(s => s.Id == examResultPostDTO.StudentId))
            throw new StudentNotFoundByIdException("Student not found");

        //if (examResultPostDTO.Score > (await _examRepository.GetSingleAsync(e => e.Id == examResultPostDTO.ExamId)).maxScore)
        //    throw new ExamResultWrongScoreInputException("Input score mustn't be higher than max score");

        var newExamResult = _mapper.Map<ExamResult>(examResultPostDTO);
        await _examResultRepository.CreateAsync(newExamResult);
        await _examResultRepository.SaveAsync();
    }

    public async Task UpdateExamResultAsync(Guid id, ExamResultPutDTO putExamResultDTO)
    {
        var examResult = await _examResultRepository.GetSingleAsync(e => e.Id == id);

        if (examResult is null)
            throw new ExamResultNotFoundByIdException("Exam's result not found");

        if (!await _examRepository.isExsistAsync(e => e.Id == putExamResultDTO.ExamId))
            throw new ExamNotFoundByIdException("Exam not found");

        if (!await _studentRepository.isExsistAsync(s => s.Id == putExamResultDTO.StudentId))
            throw new StudentNotFoundByIdException("Student not found");

        //if (putExamResultDTO.Score > (await _examRepository.GetSingleAsync(e => e.Id == putExamResultDTO.ExamId)).maxScore)
        //    throw new ExamResultWrongScoreInputException("Input score mustn't be higher than max score");

        examResult = _mapper.Map(putExamResultDTO, examResult);
        _examResultRepository.Update(examResult);
        await _examResultRepository.SaveAsync();
    }

    public async Task DeleteExamResultAsync(Guid id)
    {
        var examResult = await _examResultRepository.GetSingleAsync(e => e.Id == id);

        if (examResult is null)
            throw new ExamResultNotFoundByIdException("Exam result not found");

        _examResultRepository.Delete(examResult);
        await _examResultRepository.SaveAsync();
    }
}