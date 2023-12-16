namespace StudentManagementSystemUNEC.Business.DTOs.ExamResultDTOs;

public class ExamResultPutDTO
{
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public byte? Score { get; set; }
}