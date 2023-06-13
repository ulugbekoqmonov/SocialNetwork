using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<User>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Response<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User mappedUser = _mapper.Map<User>(request);
        User user = await _userRepository.CreateAsync(mappedUser);
        return new Response<User>(user);
    }
}
