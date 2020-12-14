using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysModelBank.Areas.Admin.Models.UserManagement
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
