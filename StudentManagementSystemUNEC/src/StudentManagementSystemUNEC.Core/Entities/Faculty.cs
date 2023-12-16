using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class Faculty : BaseSectionEntity
{
    public string Name { get; set; }
    //public FacultyCode facultyCode { get; set; }
    //public StudySector studySector { get; set; }
    public string facultyCode { get; set; }
    public string studySectorName { get; set; }
    public string studySectorCode { get; set; }
    public ICollection<Group>? Groups { get; set; }
}
