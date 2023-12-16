using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class GroupSubjectRepository : Repository<GroupSubject>, IGroupSubjectRepository
{
    public GroupSubjectRepository(AppDbContext context) : base(context)
    {
    }
}
