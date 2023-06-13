using Application.Abstraction;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repository;

public class PostRepository:IPostRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PostRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post?> CreateAsync(Post entity)
    {
        _dbContext.Posts.Add(entity);        
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Post? Post = _dbContext.Posts.FirstOrDefault(r => r.Id == id);
        _dbContext.Posts.Remove(Post);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<Post>> GetAllAsync()
    {
        IQueryable<Post> queryable = _dbContext.Posts;
        return Task.FromResult(queryable);
    }

    public async Task<Post?> GetAsync(Expression<Func<Post, bool>> expression)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(expression);
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        Post? Post = await _dbContext.Posts.FirstOrDefaultAsync(r => r.Id.Equals(id));
        return Post;
    }

    public async Task<Post?> UpdateAsync(Post entity)
    {
        _dbContext.Posts.Update(entity);
        await _dbContext.SaveChangesAsync();        
        return entity;
    }
}
