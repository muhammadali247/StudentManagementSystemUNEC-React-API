using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.ExampTypeDTOs;

public class ExamTypePostDTO
{
    public ExamTypeName Name { get; set; }
    public byte maxScore { get; set; }
}