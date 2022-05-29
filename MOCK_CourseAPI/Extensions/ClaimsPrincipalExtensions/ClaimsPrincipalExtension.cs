using System;
using System.Security.Claims;

namespace CourseAPI.Extensions.ControllerBase
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
        }
    }
}
