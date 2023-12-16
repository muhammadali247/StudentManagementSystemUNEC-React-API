using StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;

public class ExamResultGetDTO
{
    public Guid Id { get; set; }
    public string studentName { get; set; }
    public string studentSurname { get; set; }
    public ExamGetPartialForExamResultDTO Exam { get; set; }
    public byte? Score { get; set; }
}