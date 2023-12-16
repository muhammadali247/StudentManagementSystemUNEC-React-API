using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class StudentGroupRepository : Repository<StudentGroup>, IStudentGroupRepository
{
    public StudentGroupRepository(AppDbContext context) : base(context)
    {
    }
}
