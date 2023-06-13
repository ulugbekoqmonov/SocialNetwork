using Application.Users.CreateUser;
using Application.Users.GetAllUsers;
using Application.Users.GetByIdUser;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase<User>
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<Response<User>>> CreateUser([FromBody] CreateUserCommand command)
    {
        Response<User> response = await _mediator.Send(command);
        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    //[ResponseCache(Duration = 30)]
    public async Task<ActionResult<Response<IQueryable<User>>>> GetAllUsers()
    {
        Response<IQueryable<User>> response = await _mediator.Send(new GetAllUsersQuery());
        return Ok(response);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<Response<User>>> GetByIdUser(GetByIdUserQuery query)
    {
        Response<User> response = await _mediator.Send(query);
        return Ok(response);
    }
}