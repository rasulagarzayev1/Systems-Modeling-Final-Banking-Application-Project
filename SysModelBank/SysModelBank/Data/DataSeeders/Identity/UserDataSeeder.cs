using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using System;

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
                    PasswordHash = "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw==",
                    SecurityStamp = "b3af7d63-7742-4060-85ba-dd41e57f6265",
                    Address = "Brazil",
                    Status = UserStatus.Active,
                    Firstname = "Ulno",
                    Lastname = "Best"
                },
                new User
                {
                    Id = User.IshayaId,
                    Email = "ishaya@sys.ee",
                    UserName = "ishaya",
                    NormalizedUserName = "ISHAYA",
                    NormalizedEmail = "ISHAYA@SYS.EE",
                    PasswordHash = "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw==",
                    SecurityStamp = "3cbf6e9f-9c59-4674-90e7-ec85ce898399",
                    Address = "Tartu",
                    Status = UserStatus.Active,
                    Firstname = "Ishaya",
                    Lastname = "Good"
                }
            );
        }
    }
}
