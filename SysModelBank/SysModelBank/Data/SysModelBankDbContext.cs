using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.DataSeeders.Identity;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data
{
    public class SysModelBankDbContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        public SysModelBankDbContext(DbContextOptions<SysModelBankDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(SysModelBankDbContext).Assembly);

            new UserDataSeeder().Add(builder);
            new RoleDataSeeder().Add(builder);
            new UserRoleDataSeeder().Add(builder);
        }
    }
}
