using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data.DataSeeders.Identity
{
    public class UserRoleDataSeeder
    {
        public void Add(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = Role.ClientId,
                    UserId = User.UlnoId
                },
                new IdentityUserRole<int>
                {
                    RoleId = Role.AdminId,
                    UserId = User.IshayaId
                }
            );
        }
    }
}
