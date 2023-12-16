using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;

public class ExamTypeGetDTO
{
    public Guid Id { get; set; }
    public ExamTypeName Name { get; set; }
    public byte maxScore { get; set; }
}