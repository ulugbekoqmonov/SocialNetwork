using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Comments.GetByIdComment;

public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQuery, Response<Comment>>
{
    private readonly ICommentRepository _commentRepository;

    public GetByIdCommentQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response<Comment>> Handle(GetByIdCommentQuery request, CancellationToken cancellationToken)
    {
        Comment comment = await _commentRepository.GetByIdAsync(request.CommentId);
        return new Response<Comment>(comment);
    }
}
