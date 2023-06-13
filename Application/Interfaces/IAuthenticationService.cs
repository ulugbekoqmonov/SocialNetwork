using Domain.Models;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public Task<bool> IsRegistered(UserCredential userCredential);
}