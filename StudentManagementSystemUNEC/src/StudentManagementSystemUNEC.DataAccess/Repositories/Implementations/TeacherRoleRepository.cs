using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class TeacherRoleRepository : Repository<TeacherRole>, ITeacherRoleRepository
{
    public TeacherRoleRepository(AppDbContext context) : base(context)
    {
    }
}