using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Comments.GetAllComments;

public class GetAllCommentsQuery:IRequest<Response<IQueryable<Comment>>>
{
}
