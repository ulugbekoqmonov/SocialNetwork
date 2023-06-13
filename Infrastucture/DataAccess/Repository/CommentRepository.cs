using Application.Abstraction;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repository;

public class CommentRepository:ICommentRepository
{
    private readonly IApplicationDbContext _dbContext;

    public CommentRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment?> CreateAsync(Comment entity)
    {
        _dbContext.Comments.Add(entity);        
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Comment? Comment = _dbContext.Comments.FirstOrDefault(r => r.Id == id);
        _dbContext.Comments.Remove(Comment);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<Comment>> GetAllAsync()
    {
        IQueryable<Comment> queryable = _dbContext.Comments;
        return Task.FromResult(queryable);
    }

    public async Task<Comment?> GetAsync(Expression<Func<Comment, bool>> expression)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(expression);
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        Comment? Comment = await _dbContext.Comments.FirstOrDefaultAsync(r => r.Id.Equals(id));
        return Comment;
    }

    public async Task<Comment?> UpdateAsync(Comment entity)
    {
        _dbContext.Comments.Update(entity);        
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
