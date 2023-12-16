using StudentManagementSystemUNEC.Core.Entities.Common;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Exam : BaseSectionEntity
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ExamType ExamType { get; set; }
    public Guid ExamTypeId { get; set; }
    public GroupSubject GroupSubject { get; set; }
    public Guid GroupSubjectId { get; set; }
    public List<ExamResult>? ExamResults { get; set; }
    //public byte maxScore { get; set; }
}