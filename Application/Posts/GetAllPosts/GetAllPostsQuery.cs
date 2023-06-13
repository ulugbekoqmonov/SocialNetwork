using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.GetAllPosts;

public class GetAllPostsQuery:IRequest<Response<IQueryable<Post>>>
{
}
