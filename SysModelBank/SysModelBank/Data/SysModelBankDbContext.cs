using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data
{
    public class SysModelBankDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SysModelBankDbContext(DbContextOptions<SysModelBankDbContext> options)
            : base(options)
        {
        }
    }
}
