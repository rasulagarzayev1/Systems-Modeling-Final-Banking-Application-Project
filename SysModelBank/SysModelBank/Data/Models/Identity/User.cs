using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysModelBank.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysModelBank.Data.Models.Identity
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public const int UlnoId = 1;

        public const int IshayaId = 2;

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public UserStatus Status { get; set; } = UserStatus.PendingVerification;

        public virtual ICollection<Account> Accounts { get; set; }
    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Accounts).WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
