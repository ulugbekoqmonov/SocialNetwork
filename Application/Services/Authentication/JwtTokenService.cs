using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;    

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;        
    }
    public Task<Token> GenerateToken(User user)
    {
        return GenerateJWTTokens(user);
    }

    public Task<Token> GenerateRefreshToken(User user)
    {
        return GenerateJWTTokens(user);
    }

    private Task<Token> GenerateJWTTokens(User user)
    {
        try
        {
            var accessTokenLifeTime = double.Parse(_configuration["JWT:AccessTokenLifeTime"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserName)
                }),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(accessTokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
            };
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken().Result;
            return Task.FromResult(new Token
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Task<string> GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Task.FromResult(Convert.ToBase64String(randomNumber));
    }

    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JWT:Audience"],
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principial = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principial;
    }
}