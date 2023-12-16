using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;

public class ExamGetPartialForExamResultDTO
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string ExamTypeName { get; set; }
    public GroupSubjectGetPartialForExamResultDTO GroupSubject { get; set; }
}