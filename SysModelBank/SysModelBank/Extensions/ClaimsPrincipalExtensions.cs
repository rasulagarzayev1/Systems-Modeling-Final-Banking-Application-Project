using System.Security.Claims;

namespace SysModelBank.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int Id(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
