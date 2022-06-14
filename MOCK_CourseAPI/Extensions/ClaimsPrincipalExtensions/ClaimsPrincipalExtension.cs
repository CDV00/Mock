using System;
using System.Security.Claims;

namespace CourseAPI.Extensions.ControllerBase
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            if (userId == null)
                return Guid.Empty;
            return Guid.Parse(userId);
        }
    }
}
