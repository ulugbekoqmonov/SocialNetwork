using Application.Abstraction;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;

namespace Infrastructure.DataAccess.Repository;

public class UserTokenRepository : IUserTokenRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserTokenRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(UserRefreshToken entity)
    {
        _dbContext.UserRefreshTokens.Add(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string userName)
    {
        UserRefreshToken? userRefreshToken = _dbContext.UserRefreshTokens.FirstOrDefault(t => t.UserName == userName);
        _dbContext.UserRefreshTokens.Remove(userRefreshToken);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<UserRefreshToken>> GetAllAsync()
    {
        IQueryable<UserRefreshToken> queryable = _dbContext.UserRefreshTokens;
        return Task.FromResult(queryable);
    }

    public Task<UserRefreshToken?> GetAsync(string userName, Token token)
    {
        UserRefreshToken? userRefreshToken = _dbContext.UserRefreshTokens.FirstOrDefault(x => x.UserName == userName && x.RefreshToken == token.RefreshToken);
        return Task.FromResult(userRefreshToken);
    }

    public async Task<bool> UpdateAsync(UserRefreshToken entity)
    {
        UserRefreshToken? userRefreshToken = _dbContext.UserRefreshTokens.FirstOrDefault(x => x.UserName == entity.UserName);
        userRefreshToken.RefreshToken = entity.RefreshToken;
        userRefreshToken.ExpiredTime = entity.ExpiredTime;
        _dbContext.UserRefreshTokens.Update(userRefreshToken);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}