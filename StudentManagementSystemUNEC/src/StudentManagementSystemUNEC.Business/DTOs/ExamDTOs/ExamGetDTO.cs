using StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;
using StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

namespace StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;

public class ExamGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string ExamType { get; set; }
    public byte maxScore { get; set; }
    public GroupSubjectGetPartialForExamDTO GroupSubject { get; set; }
    public List<ExamResultGetPartialForExamDTO>? ExamResults { get; set; }
}