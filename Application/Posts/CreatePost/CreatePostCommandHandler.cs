using Application.Interfaces;
using Application.Notification.PostNotification;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper, IMediator mediator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Response<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Post mappedPost = _mapper.Map<Post>(request);
        Post post = await _postRepository.CreateAsync(mappedPost);
        await _mediator.Publish(new CreatePostNotification { Post = post }, cancellationToken);
        return new Response<Post>(post);
    }
}