using Microsoft.AspNetCore.Identity;
using SysModelBank.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SysModelBank.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public const int UlnoId = 1;

        public const int IshayaId = 2;

        [Required]
        public string Address { get; set; }

        public UserStatus Status { get; set; } = UserStatus.PendingVerification;
    }
}
