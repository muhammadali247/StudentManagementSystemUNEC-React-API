using Microsoft.EntityFrameworkCore;
using StudentManagementSystemUNEC.Core.Entities.Common;
using StudentManagementSystemUNEC.DataAccess.Contexts;
using StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _table;

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public IQueryable<T> GetAll(bool isTracking = false, params string[]? includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return isTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool isTracking = false, params string[]? includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return isTracking ? query.Where(expression) : query.AsNoTracking().Where(expression);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTracking = false, params string[]? includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return isTracking ? await query.FirstOrDefaultAsync(expression) : await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task CreateAsync(T entity) => await _table.AddAsync(entity);

    public void Update(T entity)
    {
        _table.Update(entity);
    }

    public void Delete(T entity) => _table.Remove(entity);

    public void SoftDelete(T entity) {
        entity.IsDeleted = true;
    } 

    public async Task<bool> isExsistAsync(Expression<Func<T, bool>> expression, params string[]? includes)
    {
        var query = _table.AsQueryable();

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await _table.AnyAsync(expression);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void DeleteList(List<T> entities)
    {
        _table.RemoveRange(entities);
    }

    public void AddList(List<T> entities)
    {
        _table.AddRange(entities);
    }

    public void Detach(T entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Detached;
    }
}
