using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.GetByIdPost;

public class GetByIdPostQuery:IRequest<Response<Post>>
{
    public Guid PostId { get; set; }
}
