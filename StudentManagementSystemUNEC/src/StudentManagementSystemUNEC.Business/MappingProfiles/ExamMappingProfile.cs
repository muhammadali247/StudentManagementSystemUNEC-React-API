using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class ExamMappingProfile : Profile
{
	public ExamMappingProfile()
	{
        CreateMap<ExamPostDTO, Exam>()
           .ForMember(e => e.Name, x => x.MapFrom(dto => dto.Name))
           .ForMember(e => e.Date, x => x.MapFrom(dto => dto.Date))
           .ForMember(e => e.GroupSubjectId, x => x.MapFrom(dto => dto.GroupSubjectId))
           .ForMember(e => e.ExamTypeId, x => x.MapFrom(dto => dto.ExamTypeId))
           //.ForMember(e => e.maxScore, x => x.MapFrom(dto => dto.maxScore))
           .ReverseMap();

        CreateMap<Exam, ExamGetDTO>()
           .ForMember(dto => dto.Id, x => x.MapFrom(e => e.Id))
           .ForMember(dto => dto.Name, x => x.MapFrom(e => e.Name))
           .ForMember(dto => dto.Date, x => x.MapFrom(e => e.Date))
           .ForMember(dto => dto.ExamType, x => x.MapFrom(e => e.ExamType.Name))
           .ForMember(dto => dto.maxScore, x => x.MapFrom(e => e.ExamType.maxScore))
           .ReverseMap();

        CreateMap<Exam, ExamGetPartialForExamResultDTO>()
           .ForMember(dto => dto.ExamTypeName, x => x.MapFrom(e => e.ExamType.Name))
           .ForMember(dto => dto.Name, x => x.MapFrom(e => e.Name))
           .ForMember(dto => dto.Date, x => x.MapFrom(e => e.Date))
           .ReverseMap();

        CreateMap<ExamPutDTO, Exam>()
           .ForMember(e => e.Name, x => x.MapFrom(dto => dto.Name))
           .ForMember(e => e.Date, x => x.MapFrom(dto => dto.Date))
           .ForMember(e => e.GroupSubjectId, x => x.MapFrom(dto => dto.GroupSubjectId))
           .ReverseMap();
    }
}