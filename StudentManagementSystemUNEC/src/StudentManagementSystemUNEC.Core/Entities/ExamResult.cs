using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class ExamResult : BaseSectionEntity
{
    public Student Student { get; set; }
    public Guid StudentId { get; set; }
    public Exam Exam { get; set; }
    public Guid ExamId { get; set; }
    public byte? Score { get; set; }
}