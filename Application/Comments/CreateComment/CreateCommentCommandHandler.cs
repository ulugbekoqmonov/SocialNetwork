using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Comments.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<Comment>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<Response<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Comment mappedComment = new Comment()
        {
            Text=request.Text,
            UserId= request.UserId,
            PostId= request.PostId,
            ReplyCommentId=request.CommentId
        };
        Comment comment = await _commentRepository.CreateAsync(mappedComment);
        return new Response<Comment>(comment);
    }
}
