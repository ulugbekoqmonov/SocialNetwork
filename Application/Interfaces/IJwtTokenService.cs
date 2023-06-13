using Domain.Models;
using Domain.Models.Entities;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IJwtTokenService
{        
    public Task<Token> GenerateToken(User user);
    public Task<Token> GenerateRefreshToken(User user);
    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
}