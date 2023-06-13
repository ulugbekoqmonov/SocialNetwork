using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IRepository<T>
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid Id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid Id);
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
}