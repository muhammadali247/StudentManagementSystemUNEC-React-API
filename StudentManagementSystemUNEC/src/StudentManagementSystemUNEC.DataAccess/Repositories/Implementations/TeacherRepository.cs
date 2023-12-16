using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    public TeacherRepository(AppDbContext context) : base(context)
    {
    }
}
