using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysModelBank.Data.DataSeeders
{
    public class AccountDataSeeder
    {
        public void Add(ModelBuilder builder)
        {
            builder.Entity<Account>().HasData(
                new Account
                {
                    Id = Account.SystemId,
                    UserId = User.SystemId,
                    Balance = 0
                }
            );
        }
    }
}
