using Microsoft.AspNetCore.Identity;

namespace SysModelBank.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public const int UlnoId = 1;

        public const int IshayaId = 2;
    }
}
