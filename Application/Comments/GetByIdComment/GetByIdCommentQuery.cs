using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Comments.GetByIdComment;

public class GetByIdCommentQuery:IRequest<Response<Comment>>
{
    public Guid CommentId { get; set; }
}
