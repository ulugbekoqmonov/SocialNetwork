using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _accountRepository;

    public AuthenticationService(IUserRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> IsRegistered(UserCredential userCredential)
    {
        IQueryable<User> accounts = await _accountRepository.GetAllAsync();
        bool result = accounts.Any(acc => acc.UserName.Equals(userCredential.UserName) &&
        acc.Password.Equals(userCredential.Password));
        return result;
    }
}