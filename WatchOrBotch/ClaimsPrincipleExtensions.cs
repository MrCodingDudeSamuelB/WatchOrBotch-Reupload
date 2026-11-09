using System.Security.Claims;

namespace WatchOrBotch
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User is not authenticated");
    }
}


