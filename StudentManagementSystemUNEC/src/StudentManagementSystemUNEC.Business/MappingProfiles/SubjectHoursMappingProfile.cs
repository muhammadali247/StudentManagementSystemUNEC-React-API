using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.SubjectHoursDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class SubjectHoursMappingProfile : Profile
{
	public SubjectHoursMappingProfile()
	{
		CreateMap<SubjectHoursPostDTO, SubjectHours>()
            .ForMember(sh => sh.GroupSubjectId, x => x.Ignore())
            .ForMember(sh => sh.LessonTypeId, x => x.MapFrom(dto => dto.GroupSubjectId))
            .ForMember(sh => sh.DayOfWeek, x => x.MapFrom(dto => dto.DayOfWeek))
            .ForMember(sh => sh.Room, x => x.MapFrom(dto => dto.Room))
            .ForMember(sh => sh.StartTime, x => x.MapFrom(dto => dto.StartTime))
            .ForMember(sh => sh.EndTime, x => x.MapFrom(dto => dto.EndTime))
            .ReverseMap();
    }
}