using Application.Abstraction;
using Application.Extersion;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateAsync(User entity)
    {
        User user = new User();
        entity.Password = entity.Password.ComputeHash();
        var entry = _dbContext.Users.Add(entity);
        await _dbContext.SaveChangesAsync();
        if(entry.State is EntityState.Added)
        {
            user = entry.Entity as User;
        }        
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
        if(user != null)
        {
            var entry = _dbContext.Users.Remove(user);
            bool res = entry.State == EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return res;
        }
        else
        {
            throw new ();
        }
    }

    public Task<IQueryable<User>> GetAllAsync()
    {
        IQueryable<User> queryable = _dbContext.Users;
        return Task.FromResult(queryable);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        User? account = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return account;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        entity.Password = entity.Password.ComputeHash();
        _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(expression);
    }

    public Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null)
    {
        if (expression is null)
        {
            return Task.FromResult(_dbContext.Users.Where(x=>true));
        }
        else return Task.FromResult(_dbContext.Users.Where(expression));
    }
}