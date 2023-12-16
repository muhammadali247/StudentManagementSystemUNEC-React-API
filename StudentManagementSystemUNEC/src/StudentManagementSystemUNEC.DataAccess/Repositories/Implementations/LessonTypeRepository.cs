using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class LessonTypeRepository : Repository<LessonType>, ILessonTypeRepository
{
    public LessonTypeRepository(AppDbContext context) : base(context)
    {
    }
}