using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class ExamTypeMappingProfile : Profile
{
	public ExamTypeMappingProfile()
	{
        CreateMap<ExamTypePostDTO, ExamType>()
            .ForMember(et => et.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(et => et.maxScore, x => x.MapFrom(dto => dto.maxScore))
            .ReverseMap();

        CreateMap<ExamType, ExamTypeGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(et => et.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(et => et.Name))
            .ForMember(dto => dto.maxScore, x => x.MapFrom(et => et.maxScore))
            .ReverseMap();

        CreateMap<ExamTypePutDTO, ExamType>()
            .ForMember(et => et.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(et => et.maxScore, x => x.MapFrom(dto => dto.maxScore))
            .ReverseMap();
    }
}