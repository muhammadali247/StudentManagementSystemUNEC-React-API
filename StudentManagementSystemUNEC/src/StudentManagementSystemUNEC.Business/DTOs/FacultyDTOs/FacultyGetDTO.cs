using StudentManagementSystemUNEC.Business.DTOs.GroupDTOs;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Business.DTOs.FacultyDTOs;

public class FacultyGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //public FacultyCode facultyCode { get; set; }
    //public StudySector studySector { get; set; }
    public string facultyCode { get; set; }
    public string studySectorName { get; set; }
    public string studySectorCode { get; set; }
    public ICollection<GroupGetPartialDTO>? Groups { get; set; }
    public ICollection<string> GroupNames { get; set; }
}
