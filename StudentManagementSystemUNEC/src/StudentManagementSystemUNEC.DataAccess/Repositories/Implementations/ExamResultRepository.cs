﻿using StudentManagementSystemUNEC.Core.Entities;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class ExamResultRepository : Repository<ExamResult>, IExamResultRepository
{
    public ExamResultRepository(AppDbContext context) : base(context)
    {
    }
}