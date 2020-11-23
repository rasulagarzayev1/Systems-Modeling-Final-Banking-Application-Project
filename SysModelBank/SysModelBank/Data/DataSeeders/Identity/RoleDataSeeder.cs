using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data.DataSeeders.Identity
{
    public class RoleDataSeeder
    {
        public void Add(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = Role.ClientId,
                    Name = "client",
                    NormalizedName = "CLIENT"
                },
                new Role
                {
                    Id = Role.AdminId,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}
