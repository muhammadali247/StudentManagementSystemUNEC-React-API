using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Business.DTOs.SubjectDTOs;
using StudentManagementSystemUNEC.Business.DTOs.TeacherDTOs;
using StudentManagementSystemUNEC.Core.Entities;

namespace StudentManagementSystemUNEC.Business.DTOs.GroupSubjectDTOs;

public class GroupSubjectGetDTO
{
    public Guid Id { get; set; }
    //public SubjectGetDTO Subject { get; set; }
    public string SubjectName { get; set; }
    public string subjectCode { get; set; }
    public string Semester { get; set; }
    //public GroupGetPartialForGroupSubjectDTO Group { get; set; }
    public string GroupName { get; set; }
    public string GroupFaculty { get; set; }
    public string GroupCreationYear { get; set; }
    public string StudentCount { get; set; }
    //public List<TeacherGetPartialDTO>? teachers { get; set; }
    //public List<Teacher> Teachers { get; set; }
    public List<string> TeacherNames { get; set; }
    public List<string> TeacherSurnames { get; set; }
    public byte Credits { get; set; }
    public short totalHours { get; set; }
    //// New property to concatenate TeacherNames and TeacherSurnames
    //public List<string> TeacherFullNames
    //{
    //    get
    //    {
    //        if (Teachers != null && Teachers.Count > 0)
    //        {
    //            List<string> fullNames = new List<string>();
    //            foreach (var teacher in Teachers)
    //            {
    //                fullNames.Add($"{teacher.Name} {teacher.Surname}");
    //            }
    //            return fullNames;
    //        }
    //        return new List<string>();
    //    }
    //}
    // New property to concatenate TeacherNames and TeacherSurnames
    public List<string> TeacherFullNames
    {
        get
        {
            if (TeacherNames != null && TeacherSurnames != null && TeacherNames.Count == TeacherSurnames.Count)
            {
                List<string> fullNames = new List<string>();
                for (int i = 0; i < TeacherNames.Count; i++)
                {
                    fullNames.Add($"{TeacherNames[i]} {TeacherSurnames[i]}");
                }
                return fullNames;
            }
            return new List<string>();
        }
    }
}