using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.Core.Utils.Enums;

namespace StudentManagementSystemUNEC.Core.Entities;

public class LessonType : BaseSectionEntity
{
    public LessonTypeName Name { get; set; }
}