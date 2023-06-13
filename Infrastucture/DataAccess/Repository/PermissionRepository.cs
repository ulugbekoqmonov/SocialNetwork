using Application.Abstraction;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repository;

public class PermissionRepository : IPermissionRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PermissionRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Permission?> CreateAsync(Permission entity)
    {
        _dbContext.Permissions.Add(entity);
        await _dbContext.SaveChangesAsync();
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p=>p.PermissionName.Equals(entity.PermissionName));
        return permission;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Id == id);
        _dbContext.Permissions.Remove(permission);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<Permission>> GetAllAsync()
    {
        IQueryable<Permission> queryable = _dbContext.Permissions;
        return Task.FromResult(queryable);
    }

    public Task<Permission> GetAsync(Expression<Func<Permission, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(x => x.Id == id);
        return permission;
    }

    public async Task<Permission?> UpdateAsync(Permission entity)
    {
        _dbContext.Permissions.Update(entity);
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));
        await _dbContext.SaveChangesAsync();
        return permission;
    }
}