using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.TeacherRoleDtos;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class TeacherRoleMappingProfile : Profile
{
	public TeacherRoleMappingProfile()
	{
		CreateMap<TeacherRolePostDTO, TeacherRole>()
			.ForMember(tr => tr.Name, x => x.MapFrom(dto => dto.Name))
			.ReverseMap();

		CreateMap<TeacherRole, TeacherRoleGetDTO>()
			.ForMember(dto => dto.Id, x => x.MapFrom(tr => tr.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(tr => tr.Name))
            .ReverseMap();

        CreateMap<TeacherRolePutDTO, TeacherRole>()
			.ForMember(tr => tr.Name, x => x.MapFrom(dto => dto.Name))
			.ReverseMap();
    }
}