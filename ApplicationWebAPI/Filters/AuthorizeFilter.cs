using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebUI.Filters;

public class AuthorizeFilter : IAuthorizationFilter
{        
    readonly Claim _claim;

    public AuthorizeFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}
