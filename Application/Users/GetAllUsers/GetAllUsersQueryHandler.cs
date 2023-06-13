using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IQueryable<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<IQueryable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> users = await _userRepository.GetAllAsync();
        return new Response<IQueryable<User>>(users);
    }
}
