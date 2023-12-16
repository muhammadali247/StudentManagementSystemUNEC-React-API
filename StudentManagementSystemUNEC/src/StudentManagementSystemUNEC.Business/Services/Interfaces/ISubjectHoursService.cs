using StudentManagementSystemUNEC.Business.DTOs.SubjectHoursDTOs;

namespace StudentManagementSystemUNEC.Business.Services.Interfaces;

public interface ISubjectHoursService
{
    Task CreateSubjectHourAsync(SubjectHoursPostDTO subjectHoursPostDTO);
}