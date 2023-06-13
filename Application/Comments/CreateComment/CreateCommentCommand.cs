using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Comments.CreateComment;

public class CreateCommentCommand : IRequest<Response<Comment>>
{
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public Guid? CommentId { get; set; }
    public Guid PostId { get; set; }
}
