using Application.Comments.CreateComment;
using Application.Comments.GetAllComments;
using Application.Comments.GetByIdComment;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ApiControllerBase<CreateCommentCommand>
{
    private readonly IMemoryCache _memoryCache;

    public CommentController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    private const string CACHE_KEY = "cacheKey";
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<Response<Comment>>> CreateComment([FromBody] CreateCommentCommand command)
    {
        Response<Comment> response = await _mediator.Send(command);

        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<Response<IQueryable<Comment>>>> GetAllComment()
    {
        Response<IQueryable<Comment>> response = new Response<IQueryable<Comment>>();
        bool isFound = _memoryCache.TryGetValue(CACHE_KEY, out IQueryable<Comment>? cacheValue);
        if (!isFound)
        {
            response = await _mediator.Send(new GetAllCommentsQuery());
            cacheValue = response.Result;
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
            _memoryCache.Set(CACHE_KEY,cacheValue,cacheEntryOptions);
        }
        else
        {
            response.Result = cacheValue;
        }
        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<Response<Comment>>> GetByIdComment(GetByIdCommentQuery query)
    {
        Response<Comment> response = await _mediator.Send(query);
        return Ok(response);
    }
}
