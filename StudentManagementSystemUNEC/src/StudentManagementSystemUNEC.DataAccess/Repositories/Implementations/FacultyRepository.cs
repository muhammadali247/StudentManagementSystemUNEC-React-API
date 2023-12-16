using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
    public FacultyRepository(AppDbContext context) : base(context)
    {
    }
}
