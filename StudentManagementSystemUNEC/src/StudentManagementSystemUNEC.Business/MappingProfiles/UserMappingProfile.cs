using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.UserDTOs;
using StudentManagementSystemUNEC.Core.Entities.Identity;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class UserMappingProfile : Profile
{
	public UserMappingProfile()
	{
        CreateMap<UserPostDTO, AppUser>()
            //.ForMember(au => au.Id, x => x.Ignore())
            .ReverseMap();

        CreateMap<studentUserPostDTO, AppUser>()
           //.ForMember(au => au.Id, x => x.Ignore())
           .ReverseMap();

        CreateMap<AppUser, UserGetDTO>()
            .ForMember(gu => gu.TeacherName, x => x.MapFrom(u => u.Teacher.Name))
            .ForMember(gu => gu.StudentName, x => x.MapFrom(u => u.Student.Name))
            .ForMember(gu => gu.EmailConfirmed, x => x.MapFrom(u => u.EmailConfirmed))
            //.ForMember(dto => dto.RoleNames, x => x.MapFrom(f => f..Select(fg => fg.Name)))
            .ForMember(gu => gu.Image, x => x.MapFrom((u, gu, _, context) =>
            {
                // Check if the user is linked to a student or teacher
                if (u.Student != null)
                {
                    // Map the image from the Student entity
                    return u.Student.Image;
                }
                else if (u.Teacher != null)
                {
                    // Map the image from the Teacher entity
                    return u.Teacher.Image;
                }

                // Handle the case where neither Student nor Teacher is present
                return null;
            }))
            .ReverseMap();

        CreateMap<AppUser, UnassignedUserGetDTO>();

        CreateMap<UserPutDTO, AppUser>()
           .ReverseMap();
    }
}