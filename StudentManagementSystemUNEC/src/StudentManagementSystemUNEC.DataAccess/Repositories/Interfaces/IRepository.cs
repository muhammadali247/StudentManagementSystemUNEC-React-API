using StudentManagementSystemUNEC.Core.Entities.Common;
using System.Linq.Expressions;

namespace StudentManagementSystemUNEC.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool isTracking = false, params string[]? includes);
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool isTracking = false, params string[]? includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTracking = false, params string[]? includes);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SoftDelete(T entity);
    void DeleteList(List<T> entities);
    void Detach(T entity);
    Task<bool> isExsistAsync(Expression<Func<T, bool>> expression, params string[]? includes);
    Task<int> SaveAsync();
    void AddList(List<T> entities);
}
