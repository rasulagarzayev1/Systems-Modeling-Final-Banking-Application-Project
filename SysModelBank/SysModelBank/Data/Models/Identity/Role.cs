using Microsoft.AspNetCore.Identity;

namespace SysModelBank.Data.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public const int ClientId = 1;

        public const int AdminId = 2;

    }
}
