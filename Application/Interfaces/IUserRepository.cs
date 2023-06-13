using Domain.Models.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IUserRepository:IRepository<User>
{
    public Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null);
}