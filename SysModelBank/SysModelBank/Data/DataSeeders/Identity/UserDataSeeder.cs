using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data.DataSeeders.Identity
{
    public class UserDataSeeder
    {
        public void Add(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = User.UlnoId,
                    Email = "ulno@sys.ee",
                    UserName = "ulno",
                    NormalizedUserName = "ULNO",
                    NormalizedEmail = "ULNO@SYS.EE",
                    PasswordHash = "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw=="
                },
                new User
                {
                    Id = User.IshayaId,
                    Email = "ishaya@sys.ee",
                    UserName = "ishaya",
                    NormalizedUserName = "ISHAYA",
                    NormalizedEmail = "ISHAYA@SYS.EE",
                    PasswordHash = "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw=="
                }
            );
        }
    }
}
