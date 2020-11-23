using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.DataSeeders.Identity;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data
{
    public class SysModelBankDbContext : IdentityDbContext<User, Role, int>
    {
        public SysModelBankDbContext(DbContextOptions<SysModelBankDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new UserDataSeeder().Add(builder);
            new RoleDataSeeder().Add(builder);
            new UserRoleDataSeeder().Add(builder);
        }
    }
}
