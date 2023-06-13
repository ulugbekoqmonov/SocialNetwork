using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.GetAllUsers;

public class GetAllUsersQuery:IRequest<Response<IQueryable<User>>>
{
}
