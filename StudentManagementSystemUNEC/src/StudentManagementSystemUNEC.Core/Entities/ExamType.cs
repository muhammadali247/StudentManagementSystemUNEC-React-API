using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class ExamType : BaseSectionEntity
{
    public ExamTypeName Name { get; set; }
    public byte maxScore { get; set; }
    public List<Exam>? Exams { get; set; }
}