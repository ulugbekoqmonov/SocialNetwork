using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Filters;

public class AuthorizationFilterAttribute : TypeFilterAttribute
{
    public AuthorizationFilterAttribute(string claimType, string claimValue) : base(typeof(AuthorizationFilterAttribute))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}
