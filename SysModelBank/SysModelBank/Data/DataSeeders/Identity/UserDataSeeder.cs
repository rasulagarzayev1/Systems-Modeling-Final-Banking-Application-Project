using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Models.Settings;
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
                    Lastname = "Best",
                    ConcurrencyStamp = "aaa20ff6-6bf2-4f3c-897f-6bf35568bf3b",
                    CurrencyId = Currency.EurId
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
                    Lastname = "Good",
                    ConcurrencyStamp = "393b36c9-83d5-4d5c-82e1-34a0d8551a68",
                    CurrencyId = Currency.EurId
                },
                new User
                {
                    Id = User.SystemId,
                    Email = "admin@admin.ee",
                    UserName = "ADMIN",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@ADMIN.EE",
                    PasswordHash = "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw==",
                    SecurityStamp = "ce907fd5-ccb4-4e96-a7ea-45712a14f5ef",
                    Address = "Tartu",
                    Status = UserStatus.Active,
                    Firstname = "admin",
                    Lastname = "admin",
                    ConcurrencyStamp = "da7a4f42-ff3c-42a8-935a-62af68f978b0",
                    CurrencyId = Currency.EurId
                }
            );
        }
    }
}
