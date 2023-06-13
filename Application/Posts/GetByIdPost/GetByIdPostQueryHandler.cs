using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.GetByIdPost;

public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQuery, Response<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetByIdPostQueryHandler(IMapper mapper, IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<Post>> Handle(GetByIdPostQuery request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.GetByIdAsync(request.PostId);
        return new Response<Post>(post);
    }
}
