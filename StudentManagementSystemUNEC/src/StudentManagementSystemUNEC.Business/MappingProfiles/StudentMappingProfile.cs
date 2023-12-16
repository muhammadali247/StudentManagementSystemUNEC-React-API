using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class StudentMappingProfile : Profile
{
	public StudentMappingProfile()
	{
		CreateMap<StudentPostDTO, Student>()
			.ForMember(s => s.Image, x => x.MapFrom(dto => dto.Image))
            .ForMember(s => s.AppUserId, x => x.MapFrom(dto => dto.AppUserId))
			//.ForMember(s => s.Email, x => x.MapFrom(dto => dto.Email))
   //         .ForMember(s => s.Username, x => x.MapFrom(dto => dto.Username))
			//.ForMember(s => s.Password, x => x.MapFrom(dto => dto.Password))
			.ForMember(s => s.Name, x => x.MapFrom(dto => dto.Name))
			.ForMember(s => s.Surname, x => x.MapFrom(dto => dto.Surname))
			.ForMember(s => s.middleName, x => x.MapFrom(dto => dto.middleName))
			.ForMember(s => s.admissionYear, x => x.MapFrom(dto => dto.admissionYear))
			.ForMember(s => s.educationStatus, x => x.MapFrom(dto => dto.educationStatus))
			.ForMember(s => s.corporativeEmail, x => x.MapFrom(dto => dto.corporativeEmail))
			.ForMember(s => s.corporativePassword, x => x.MapFrom(dto => dto.corporativePassword))
            .ForMember(s => s.Gender, x => x.MapFrom(dto => dto.Gender))
            .ForMember(s => s.Country, x => x.MapFrom(dto => dto.Country))
            .ForMember(s => s.GroupId, x => x.MapFrom(dto => dto.MainGroup))
            .ReverseMap();

        CreateMap<Student, StudentGetDTO>()
            .ForMember(gs => gs.MainGroup, x => x.MapFrom(s => s.Group))
            .ForMember(gs => gs.MainGroupName, x => x.MapFrom(s => s.Group != null ? s.Group.Name : string.Empty))
            .ForMember(gt => gt.examResults, x => x.MapFrom(s => s.examResults))
            .ForMember(gs => gs.Groups, x => x.MapFrom(s => s.studentGroups.Select(sg => new GroupGetPartialDTO { Id = sg.GroupId, Name = sg.Group.Name })))
            .ForMember(dto => dto.Id, x => x.MapFrom(s => s.Id))
            .ForMember(dto => dto.Image, x => x.MapFrom(s => s.Image))
            //.ForMember(dto => dto.Email, x => x.MapFrom(s => s.Email))
            //.ForMember(dto => dto.Username, x => x.MapFrom(s => s.Username))
            //.ForMember(dto => dto.Password, x => x.MapFrom(s => s.Password))
            .ForMember(dto => dto.Name, x => x.MapFrom(s => s.Name))
            .ForMember(dto => dto.Surname, x => x.MapFrom(s => s.Surname))
            .ForMember(dto => dto.middleName, x => x.MapFrom(s => s.middleName))
            .ForMember(dto => dto.admissionYear, x => x.MapFrom(s => s.admissionYear))
            .ForMember(dto => dto.educationStatus, x => x.MapFrom(s => s.educationStatus))
            .ForMember(dto => dto.corporativeEmail, x => x.MapFrom(s => s.corporativeEmail))
            .ForMember(dto => dto.corporativePassword, x => x.MapFrom(s => s.corporativePassword))
            .ForMember(dto => dto.Gender, x => x.MapFrom(s => s.Gender))
            .ForMember(dto => dto.GroupNames, x => x.MapFrom(s => s.studentGroups.Select(sg => sg.Group.Name)))
            .ForMember(dto => dto.Country, x => x.MapFrom(s => s.Country))
            .ForMember(dto => dto.Username, x => x.MapFrom(s => s.AppUser != null ? s.AppUser.UserName : string.Empty))
            .ReverseMap();
            
		CreateMap<Student, StudentGetPartialDTO>().ReverseMap();

		CreateMap<StudentPutDTO, Student>()
            .ForMember(s => s.Image, x => x.MapFrom(dto => dto.Image))
            //.ForMember(s => s.AppUserId, x => x.MapFrom(dto => dto.AppUserId))
            //.ForMember(s => s.Email, x => x.MapFrom(dto => dto.Email))
            //.ForMember(s => s.Username, x => x.MapFrom(dto => dto.Username))
            //.ForMember(s => s.Password, x => x.MapFrom(dto => dto.Password))
            .ForMember(s => s.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(s => s.Surname, x => x.MapFrom(dto => dto.Surname))
            .ForMember(s => s.middleName, x => x.MapFrom(dto => dto.middleName))
            .ForMember(s => s.admissionYear, x => x.MapFrom(dto => dto.admissionYear))
            .ForMember(s => s.educationStatus, x => x.MapFrom(dto => dto.educationStatus))
            .ForMember(s => s.corporativeEmail, x => x.MapFrom(dto => dto.corporativeEmail))
            .ForMember(s => s.corporativePassword, x => x.MapFrom(dto => dto.corporativePassword))
            .ForMember(s => s.Gender, x => x.MapFrom(dto => dto.Gender))
            .ForMember(s => s.Country, x => x.MapFrom(dto => dto.Country))
            .ForMember(s => s.GroupId, x => x.MapFrom(ps => ps.MainGroup))
            .ReverseMap();
	}
}