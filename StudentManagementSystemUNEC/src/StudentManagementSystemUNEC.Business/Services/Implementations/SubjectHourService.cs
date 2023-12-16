using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.SubjectHoursDTOs;
using StudentManagementSystemUNEC.Business.Exceptions.GroupSubjectExceptions;
using StudentManagementSystemUNEC.Business.Exceptions.LessonTypeExceptions;
using StudentManagementSystemUNEC.Business.Services.Interfaces;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.Business.Services.Implementations;

public class SubjectHourService : ISubjectHoursService
{
    private readonly ISubjectHourRepository _subjectHourRepository;
    private readonly IMapper _mapper;
    private readonly ILessonTypeRepository _lessonTypeRepository;
    private readonly IGroupSubjectRepository _groupSubjectRepository;

    public SubjectHourService(ISubjectHourRepository subjectHourRepository, IMapper mapper, ILessonTypeRepository lessonTypeRepository, IGroupSubjectRepository groupSubjectRepository)
    {
        _mapper = mapper;
        _subjectHourRepository = subjectHourRepository;
        _lessonTypeRepository = lessonTypeRepository;
        _groupSubjectRepository = groupSubjectRepository;
    }

    public async Task CreateSubjectHourAsync(SubjectHoursPostDTO subjectHoursPostDTO)
    {
        if (!await _subjectHourRepository.isExsistAsync(gs => gs.Id == subjectHoursPostDTO.GroupSubjectId))
            throw new GroupSubjectNotFoundByIdException("Group's subject not found");
        if (!await _groupSubjectRepository.isExsistAsync(lt => lt.Id == subjectHoursPostDTO.LessonTypeId))
            throw new LessonTypeNotFoundByIdException("Lesson's type not found");
    }
}