using Domain.Models;
using Domain.Models.Entities;

namespace Application.Interfaces;

public interface IUserTokenRepository
{
    Task<IQueryable<UserRefreshToken>> GetAllAsync();
    Task<UserRefreshToken> GetAsync(string userName, Token token);
    Task<bool> CreateAsync(UserRefreshToken entity);
    Task<bool> UpdateAsync(UserRefreshToken entity);
    Task<bool> DeleteAsync(string userName);
}