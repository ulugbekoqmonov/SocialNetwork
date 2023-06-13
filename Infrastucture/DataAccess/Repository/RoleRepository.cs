using Application.Abstraction;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IApplicationDbContext _dbContext;

    public RoleRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> CreateAsync(Role entity)
    {
        _dbContext.Roles.Add(entity);
        Role? role = await _dbContext.Roles.FirstOrDefaultAsync(r=>r.RoleName.Equals(entity.RoleName, StringComparison.OrdinalIgnoreCase));
        await _dbContext.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Role? role = _dbContext.Roles.FirstOrDefault(r => r.Id == id);
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<Role>> GetAllAsync()
    {
        IQueryable<Role> queryable = _dbContext.Roles;
        return Task.FromResult(queryable);
    }

    public Task<Role> GetAsync(Expression<Func<Role, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        Role? role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id.Equals(id));
        return role;
    }

    public async Task<Role?> UpdateAsync(Role entity)
    {
        _dbContext.Roles.Update(entity);
        Role? role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id.Equals(entity.Id));
        await _dbContext.SaveChangesAsync();
        return role;
    }
}