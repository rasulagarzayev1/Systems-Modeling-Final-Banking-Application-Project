using Microsoft.AspNetCore.Identity;

namespace SysModelBank.Data.Models.Identity
{
    public class Role : IdentityRole<int>, IBaseEntity
    {
        public const int ClientId = 1;

        public const int AdminId = 2;
        public const string Admin = "admin";

    }
}
