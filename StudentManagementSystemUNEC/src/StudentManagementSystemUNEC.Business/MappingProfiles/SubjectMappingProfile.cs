using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class SubjectMappingProfile : Profile
{
	public SubjectMappingProfile()
	{
        CreateMap<SubjectPostDTO, Subject>()
            .ForMember(s => s.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(s => s.subjectCode, x => x.MapFrom(dto => dto.subjectCode))
            .ForMember(s => s.Semester, x => x.MapFrom(dto => dto.Semester))
            .ReverseMap();

        CreateMap<Subject, SubjectGetDTO>()
           .ForMember(dto => dto.Id, x => x.MapFrom(s => s.Id))
           .ForMember(dto => dto.Name, x => x.MapFrom(s => s.Name))
           .ForMember(dto => dto.SubjectCode, x => x.MapFrom(s => s.subjectCode))
           .ForMember(dto => dto.Semester, x => x.MapFrom(s => s.Semester))
           .ReverseMap();

        CreateMap<SubjectPutDTO, Subject>()
           .ForMember(s => s.Name, x => x.MapFrom(dto => dto.Name))
           .ForMember(s => s.subjectCode, x => x.MapFrom(dto => dto.subjectCode))
           .ForMember(s => s.Semester, x => x.MapFrom(dto => dto.Semester))
           .ReverseMap();
    }
}
