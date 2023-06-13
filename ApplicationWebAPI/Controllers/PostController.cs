using Application.Posts.CreatePost;
using Application.Posts.GetAllPosts;
using Application.Posts.GetByIdPost;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ApiControllerBase<Post>
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<Response<Post>>> CreatePost([FromBody] CreatePostCommand request)
    {
        Response<Post> response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    [OutputCache(Duration = 30)]
    public async Task<ActionResult<Response<IQueryable<Post>>>> GetAllPost()
    {
        Response<IQueryable<Post>> response = await _mediator.Send(new GetAllPostsQuery());
        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<Response<Post>>> GetByIdPost(GetByIdPostQuery query)
    {
        Response<Post> response = await _mediator.Send(query);
        return Ok(response);
    }
}
