using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;

namespace Application.Posts.GetAllPosts;

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, Response<IQueryable<Post>>>
{
    private readonly IPostRepository _postRepository;

    public GetAllPostsQueryHandler(IMapper mapper, IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response<IQueryable<Post>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Post> posts = await _postRepository.GetAllAsync();
        return new Response<IQueryable<Post>>(posts);
    }
}
