using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.FacultyDTOs;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class FacultyMappingProfile : Profile
{
	public FacultyMappingProfile()
	{
        CreateMap<FacultyPostDTO, Faculty>()
            .ForMember(f => f.Name, x => x.MapFrom(dto => dto.Name))
            //.ForMember(f => f.facultyCode, x => x.MapFrom(dto => dto.facultyCode))
            //.ForMember(f => f.studySector, x => x.MapFrom(dto => dto.studySector))
            .ForMember(f => f.facultyCode, x => x.MapFrom(dto => dto.facultyCode))
            .ForMember(f => f.studySectorName, x => x.MapFrom(dto => dto.studySectorName))
            .ForMember(f => f.studySectorCode, x => x.MapFrom(dto => dto.studySectorCode))
            .ReverseMap();

        CreateMap<Faculty, FacultyGetDTO>()
			.ForMember(dto => dto.Id, x => x.MapFrom(f => f.Id))
			.ForMember(dto => dto.Name, x => x.MapFrom(f => f.Name))
            //.ForMember(dto => dto.facultyCode, x => x.MapFrom(f => f.facultyCode))
            //.ForMember(dto => dto.studySector, x => x.MapFrom(f => f.studySector))
            .ForMember(dto => dto.facultyCode, x => x.MapFrom(f => f.facultyCode))
            .ForMember(dto => dto.studySectorName, x => x.MapFrom(f => f.studySectorName))
            .ForMember(dto => dto.studySectorCode, x => x.MapFrom(f => f.studySectorCode))
            .ForMember(dto => dto.GroupNames, x => x.MapFrom(f => f.Groups.Select(fg => fg.Name)))
            .ForMember(dto => dto.Groups, x => x.MapFrom(f => f.Groups.Select(fg => new GroupGetPartialDTO
            {
                Id = fg.Id,
                Name = fg.Name,
                StudentCount = fg.StudentCount,
                CreationYear = fg.CreationYear
            })))
            .ReverseMap();

        CreateMap<FacultyPutDTO, Faculty>()
            .ForMember(f => f.Name, x => x.MapFrom(dto => dto.Name))
            //.ForMember(f => f.facultyCode, x => x.MapFrom(dto => dto.facultyCode))
            //.ForMember(f => f.studySector, x => x.MapFrom(dto => dto.studySector))
            .ForMember(f => f.facultyCode, x => x.MapFrom(dto => dto.facultyCode))
            .ForMember(f => f.studySectorName, x => x.MapFrom(dto => dto.studySectorName))
            .ForMember(f => f.studySectorCode, x => x.MapFrom(dto => dto.studySectorCode))
            .ReverseMap();
    }
}
