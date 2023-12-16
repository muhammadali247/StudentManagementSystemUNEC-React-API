using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystemUNEC.Business.DTOs.RoleDTOs;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class RoleMappingProfile : Profile
{
	public RoleMappingProfile()
	{
        CreateMap<IdentityRole, RoleGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(ir => ir.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(ir => ir.Name))
            .ReverseMap();
    }
}