using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public abstract class ApiControllerBase<T>:ControllerBase
    {
        protected IMediator _mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
