using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.CreateUser;

public class CreateUserCommand : IRequest<Response<User>>
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
