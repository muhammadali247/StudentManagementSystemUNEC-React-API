using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.DTOs.StudentDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class GroupMappingProfile : Profile
{
	public GroupMappingProfile()
	{
        CreateMap<GroupPostDTO, Group>()
            .ForMember(g => g.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(g => g.CreationYear, x => x.MapFrom(dto => dto.CreationYear))
            .ForMember(g => g.FacultyId, x => x.MapFrom(dto => dto.FacultyId))
            .ForMember(g => g.StudentGroups, x => x.MapFrom(dto => dto.StudentIds.Select(Id => new StudentGroup { StudentId = Id, subGroup = (Core.Utils.Enums.SubGroup)dto.subGroup})))
            .ForMember(g => g.StudentCount, x => x.MapFrom(dto => dto.StudentIds.Count))
            .ReverseMap();

        CreateMap<Group, GroupGetDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(g => g.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(g => g.Name))
            .ForMember(dto => dto.CreationYear, x => x.MapFrom(g => g.CreationYear))
            .ForMember(dto => dto.StudentCount, x => x.MapFrom(g => g.StudentCount))
            .ForMember(dto => dto.facultyName, x => x.MapFrom(g => g.Faculty.Name))
            //.ForMember(dto => dto.Students, x => x.MapFrom(g => g.StudentGroups.Select(sg => new StudentGetPartialDTO { Id = sg.Student.Id, 
            //    Username = sg.Student.Username, Name = sg.Student.Name, Surname = sg.Student.Surname})))
            .ForMember(dto => dto.Students, x => x.MapFrom(g => g.StudentGroups.Select(sg => new StudentGetPartialDTO
            {
                Id = sg.Student.Id,
                //Username = sg.Student.Username,
                Name = sg.Student.Name,
                Surname = sg.Student.Surname
            })))
            .ReverseMap();

        CreateMap<Group, GroupGetPartialDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(g => g.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(g => g.Name))
            .ForMember(dto => dto.StudentCount, x => x.MapFrom(g => g.StudentCount))
            .ForMember(dto => dto.CreationYear, x => x.MapFrom(g => g.CreationYear))
            .ReverseMap();

        CreateMap<Group, GroupGetPartialForGroupSubjectDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(g => g.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(g => g.Name))
            .ForMember(dto => dto.CreationYear, x => x.MapFrom(g => g.CreationYear))
            .ForMember(dto => dto.FacultyName, x => x.MapFrom(g => g.Faculty.Name))
            .ForMember(dto => dto.Students, x => x.MapFrom(g => g.StudentGroups.Select(sg => new StudentGetPartialDTO
            {
                Id = sg.Student.Id,
                //Username = sg.Student.Username,
                Name = sg.Student.Name,
                Surname = sg.Student.Surname
            })))
            .ReverseMap();

        CreateMap<Group, GroupGetPartialForTeacherDTO>()
             .ForMember(dto => dto.Name, x => x.MapFrom(g => g.Name))
             .ForMember(dto => dto.StudentCount, x => x.MapFrom(g => g.StudentCount))
             .ForMember(dto => dto.CreationYear, x => x.MapFrom(g => g.CreationYear))
             .ForMember(dto => dto.facultyName, x => x.MapFrom(g => g.Faculty.Name))
             .ReverseMap();

        CreateMap<Group, MainGroupGetPartialForStudentDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(g => g.Id))
            .ForMember(dto => dto.Name, x => x.MapFrom(g => g.Name))
            .ForMember(dto => dto.facultyName, x => x.MapFrom(g => g.Faculty.Name))
            .ReverseMap();

        CreateMap<GroupPutDTO, Group>()
            .ForMember(g => g.Name, x => x.MapFrom(dto => dto.Name))
            .ForMember(g => g.CreationYear, x => x.MapFrom(dto => dto.CreationYear))
            .ForMember(g => g.FacultyId, x => x.MapFrom(dto => dto.FacultyId))
            .ForMember(g => g.StudentGroups, x => x.MapFrom(dto => dto.StudentIds.Select(Id => new StudentGroup { StudentId = Id, subGroup = (Core.Utils.Enums.SubGroup)dto.subGroup })))
            .ForMember(g => g.StudentCount, x => x.MapFrom(dto => dto.StudentIds.Count))
            .ReverseMap();
    }
}
