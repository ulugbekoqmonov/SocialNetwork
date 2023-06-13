using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Comments.GetAllComments;

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, Response<IQueryable<Comment>>>
{
    private readonly IMemoryCache _memoryCache;
    private const string CACHE_KEY = "cacheKey";
    private readonly ICommentRepository _commentRepository;

    public GetAllCommentsQueryHandler(ICommentRepository commentRepository, IMemoryCache memoryCache)
    {
        _commentRepository = commentRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Response<IQueryable<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Comment> comments = await _commentRepository.GetAllAsync();
        return new Response<IQueryable<Comment>>(comments);
    }
}
