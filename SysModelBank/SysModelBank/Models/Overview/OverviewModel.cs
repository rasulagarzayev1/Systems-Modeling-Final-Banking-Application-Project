using SysModelBank.Data.Models;
using System.Collections.Generic;

namespace SysModelBank.Models.Overview
{
    public class OverviewModel
    {
        public IEnumerable<Account> UserAccounts { get; set; }
    }
}
