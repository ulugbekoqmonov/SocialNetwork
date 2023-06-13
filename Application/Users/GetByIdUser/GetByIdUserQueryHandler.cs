using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.GetByIdUser;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, Response<User>>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<User>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.UserId);
        return new Response<User>(user);
    }
}
