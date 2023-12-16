using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class TeacherSubjectMappingProfile : Profile
{
	public TeacherSubjectMappingProfile()
	{
        CreateMap<PostTeacherSubjectRoleDTO, TeacherSubject>().ReverseMap();
    }
}
