namespace StudentManagementSystemUNEC.Business.DTOs.ExamDTOs;

public class ExamPostDTO
{
    public string Name { get; set; }
    //public byte maxScore { get; set; }
    public DateTime Date { get; set; }
    public Guid ExamTypeId { get; set; }
    public Guid GroupSubjectId { get; set; }
}