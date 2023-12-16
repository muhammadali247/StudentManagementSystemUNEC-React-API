using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }
}