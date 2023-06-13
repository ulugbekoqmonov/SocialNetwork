using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.CreatePost;

public class CreatePostCommand : IRequest<Response<Post>>
{
    public string Content { get; set; }
    public string? Caption { get; set; }
    public Guid UserId { get; set; }
}
