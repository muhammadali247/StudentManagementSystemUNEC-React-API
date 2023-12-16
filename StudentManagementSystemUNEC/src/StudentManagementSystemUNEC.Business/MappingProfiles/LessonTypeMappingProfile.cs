using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.LessonTypeDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class LessonTypeMappingProfile : Profile
{
	public LessonTypeMappingProfile()
	{
        CreateMap<LessonTypePostDTO, LessonType>()
            .ForMember(tr => tr.Name, x => x.MapFrom(dto => dto.Name))
            .ReverseMap();

        CreateMap<LessonType, LessonTypeGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(tr => tr.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(tr => tr.Name))
            .ReverseMap();

        CreateMap<LessonTypePutDTO, LessonType>()
            .ForMember(tr => tr.Name, x => x.MapFrom(dto => dto.Name))
            .ReverseMap();
    }
}