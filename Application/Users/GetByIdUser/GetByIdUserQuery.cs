using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Users.GetByIdUser;

public class GetByIdUserQuery:IRequest<Response<User>>
{
    public Guid UserId { get; set; }
}
