using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class ExamTypeRepository : Repository<ExamType>, IExamTypeRepository
{
    public ExamTypeRepository(AppDbContext context) : base(context)
    {
    }
}