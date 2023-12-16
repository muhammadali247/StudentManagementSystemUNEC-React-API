using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class TeacherMappingProfile : Profile
{
	public TeacherMappingProfile()
	{
        CreateMap<TeacherPostDTO, Teacher>()
            .ForMember(t => t.Image, x => x.MapFrom(dto => dto.Image))
            .ForMember(t => t.AppUserId, x => x.MapFrom(dto => dto.AppUserId))
            .ForMember(t => t.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(t => t.Surname, x => x.MapFrom(dto => dto.Surname))
            .ForMember(t => t.middleName, x => x.MapFrom(dto => dto.middleName))
            .ForMember(t => t.Gender, x => x.MapFrom(dto => dto.Gender))
            .ForMember(t => t.Country, x => x.MapFrom(dto => dto.Country))
            .ForMember(t => t.BirthDate, x => x.MapFrom(dto => dto.BirthDate))
            .ReverseMap();

        CreateMap<Teacher, TeacherGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(t => t.Id))
            .ForMember(dto => dto.Image, x => x.MapFrom(t => t.Image))
            .ForMember(dto => dto.Name, x => x.MapFrom(t => t.Name))
            .ForMember(dto => dto.Surname, x => x.MapFrom(t => t.Surname))
            .ForMember(dto => dto.middleName, x => x.MapFrom(t => t.middleName))
            .ForMember(dto => dto.Gender, x => x.MapFrom(t => t.Gender))
            .ForMember(dto => dto.Country, x => x.MapFrom(t => t.Country))
            .ForMember(dto => dto.BirthDate, x => x.MapFrom(t => t.BirthDate))
            .ForMember(dto => dto.Username, x => x.MapFrom(t => t.AppUser != null ? t.AppUser.UserName : string.Empty))
            .ReverseMap();

        CreateMap<Teacher, TeacherGetPartialDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(t => t.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(t => t.Name))
            //.ForMember(dto => dto.Surname, x => x.MapFrom(t => t.Surname))
            .ReverseMap();

        CreateMap<TeacherPutDTO, Teacher>()
            .ForMember(t => t.Image, x => x.MapFrom(dto => dto.Image))
            .ForMember(t => t.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(t => t.Surname, x => x.MapFrom(dto => dto.Surname))
            .ForMember(t => t.middleName, x => x.MapFrom(dto => dto.middleName))
            .ForMember(t => t.Gender, x => x.MapFrom(dto => dto.Gender))
            .ForMember(t => t.Country, x => x.MapFrom(dto => dto.Country))
            .ForMember(t => t.BirthDate, x => x.MapFrom(dto => dto.BirthDate))
            .ReverseMap();
    }
}
