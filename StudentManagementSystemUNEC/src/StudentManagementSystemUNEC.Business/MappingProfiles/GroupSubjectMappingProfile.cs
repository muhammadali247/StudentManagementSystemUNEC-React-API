using AutoMapper;
using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.MappingProfiles;

public class GroupSubjectMappingProfile : Profile
{
	public GroupSubjectMappingProfile()
	{
		CreateMap<GroupSubject, GroupSubjectGetDTO>()
            //.ForMember(dto => dto.teachers, x => x.MapFrom(gs => gs.teacherSubjects.Select(ts => new TeacherGetPartialDTO { Id = ts.TeacherId, Name = ts.Teacher.Name, Surname = ts.Teacher.Surname, RoleName = ts.TeacherRole.Name })))
            //.ForMember(dto => dto.Teachers, x => x.MapFrom(gs => gs.teacherSubjects.Select(ts => ts.Teacher)))
            .ForMember(dto => dto.SubjectName, x => x.MapFrom(gs => gs.Subject.Name))
            .ForMember(dto => dto.subjectCode, x => x.MapFrom(gs => gs.Subject.subjectCode))
            .ForMember(dto => dto.Semester, x => x.MapFrom(gs => gs.Subject.Semester))
            .ForMember(dto => dto.GroupName, x => x.MapFrom(gs => gs.Group.Name))
            .ForMember(dto => dto.GroupFaculty, x => x.MapFrom(gs => gs.Group.Faculty.Name))
            .ForMember(dto => dto.GroupCreationYear, x => x.MapFrom(gs => gs.Group.CreationYear))
            .ForMember(dto => dto.StudentCount, x => x.MapFrom(gs => gs.Group.StudentCount))
            .ForMember(dto => dto.TeacherNames, x => x.MapFrom(gs => gs.teacherSubjects.Select(ts => ts.Teacher.Name)))
            .ForMember(dto => dto.TeacherSurnames, x => x.MapFrom(gs => gs.teacherSubjects.Select(ts => ts.Teacher.Surname)))
            .ForMember(dto => dto.Credits, x => x.MapFrom(gs => gs.Credits))
			.ForMember(dto => dto.totalHours, x => x.MapFrom(gs => gs.totalHours))
            .ReverseMap();

        CreateMap<GroupSubject, GroupSubjectGetPartialForExamDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(gs => gs.Id))
            .ForMember(dto => dto.Credits, x => x.MapFrom(gs => gs.Credits))
            .ForMember(dto => dto.groupName, x => x.MapFrom(gs => gs.Group.Name))
            .ForMember(dto => dto.subjectName, x => x.MapFrom(gs => gs.Subject.Name))
            .ReverseMap();

        CreateMap<GroupSubject, GroupSubjectGetPartialForExamResultDTO>()
           .ForMember(dto => dto.subjectName, x => x.MapFrom(gs => gs.Subject.Name))
           .ForMember(dto => dto.groupName, x => x.MapFrom(gs => gs.Group.Name))
           .ReverseMap();

        CreateMap<GroupSubject, GroupSubjectGetPartialForTeacherDTO>()
            .ForMember(dto => dto.Credits, x => x.MapFrom(gs => gs.Credits))
            .ForMember(dto => dto.totalHours, x => x.MapFrom(gs => gs.totalHours))
            .ForMember(dto => dto.subjectName, x => x.MapFrom(gs => gs.Subject.Name))
            .ReverseMap();

        CreateMap<GroupSubject, GroupSubjectGetPartialForGroupDTO>()
            .ForMember(dto => dto.Id, x => x.MapFrom(gs => gs.Id))
            .ForMember(dto => dto.totalHours, x => x.MapFrom(gs => gs.totalHours))
            .ForMember(dto => dto.Credits, x => x.MapFrom(gs => gs.Credits))
            .ReverseMap();

        CreateMap<GroupSubjectPostDTO, GroupSubject>()
            .ForMember(gs => gs.teacherSubjects, x => x.Ignore())
            .ForMember(gs => gs.Credits, x => x.MapFrom(dto => dto.Credits))
            .ForMember(gs => gs.totalHours, x => x.MapFrom(dto => dto.totalHours))
            .ForMember(gs => gs.GroupId, x => x.MapFrom(dto => dto.GroupId))
            .ForMember(gs => gs.SubjectId, x => x.MapFrom(dto => dto.SubjectId))
            .ReverseMap();

        CreateMap<GroupSubjectPutDTO, GroupSubject>()
           .ForMember(gs => gs.teacherSubjects, x => x.Ignore())
           .ForMember(gs => gs.Credits, x => x.MapFrom(dto => dto.Credits))
           .ForMember(gs => gs.totalHours, x => x.MapFrom(dto => dto.totalHours))
           .ForMember(gs => gs.GroupId, x => x.MapFrom(dto => dto.GroupId))
           .ForMember(gs => gs.SubjectId, x => x.MapFrom(dto => dto.SubjectId))
           .ReverseMap();
    }
}
