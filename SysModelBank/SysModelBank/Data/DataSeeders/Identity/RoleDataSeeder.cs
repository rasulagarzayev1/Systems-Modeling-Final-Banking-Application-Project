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
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = "df1ac39d-c136-4bb9-abd4-8f966b15a8c2"
                },
                new Role
                {
                    Id = Role.AdminId,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "9f2cef27-c862-427d-ae0e-19d40c718ed4"
                }
            );
        }
    }
}
