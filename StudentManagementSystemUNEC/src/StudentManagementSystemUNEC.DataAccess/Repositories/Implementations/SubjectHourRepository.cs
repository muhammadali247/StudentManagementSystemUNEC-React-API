using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class SubjectHourRepository : Repository<SubjectHours>, ISubjectHourRepository
{
    public SubjectHourRepository(AppDbContext context) : base(context)
    {
    }
}