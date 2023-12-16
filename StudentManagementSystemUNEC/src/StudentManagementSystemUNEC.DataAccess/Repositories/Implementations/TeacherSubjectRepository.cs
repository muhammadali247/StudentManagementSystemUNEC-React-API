using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class TeacherSubjectRepository : Repository<TeacherSubject>, ITeacherSubjectRepository
{
    public TeacherSubjectRepository(AppDbContext context) : base(context)
    {
    }
}
