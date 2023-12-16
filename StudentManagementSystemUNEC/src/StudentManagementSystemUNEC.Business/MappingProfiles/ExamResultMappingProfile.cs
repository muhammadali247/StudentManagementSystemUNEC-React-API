using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class ExamResultMappingProfile : Profile
{
	public ExamResultMappingProfile()
	{
         CreateMap<ExamResultPostDTO, ExamResult>()
            .ForMember(f => f.StudentId, x => x.MapFrom(dto => dto.StudentId))
            .ForMember(f => f.ExamId, x => x.MapFrom(dto => dto.ExamId))
            .ForMember(f => f.Score, x => x.MapFrom(dto => dto.Score))
            .ReverseMap();

        CreateMap<ExamResult, ExamResultGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(f => f.Id))
            .ForMember(dto => dto.studentName, x => x.MapFrom(f => f.Student.Name))
            .ForMember(dto => dto.studentSurname, x => x.MapFrom(f => f.Student.Surname))
            .ForMember(dto => dto.Score, x => x.MapFrom(f => f.Score))
          .ReverseMap();

        CreateMap<ExamResult, ExamResultGetPartialForExamDTO>()
            .ForMember(dto => dto.studentName, x => x.MapFrom(f => f.Student.Name))
            .ForMember(dto => dto.studentSurname, x => x.MapFrom(f => f.Student.Surname))
            .ForMember(dto => dto.Score, x => x.MapFrom(f => f.Score))
            .ReverseMap();

        CreateMap<ExamResultPutDTO, ExamResult>()
            .ForMember(f => f.StudentId, x => x.MapFrom(dto => dto.StudentId))
            .ForMember(f => f.ExamId, x => x.MapFrom(dto => dto.ExamId))
            .ForMember(f => f.Score, x => x.MapFrom(dto => dto.Score))
            .ReverseMap();
    }
}