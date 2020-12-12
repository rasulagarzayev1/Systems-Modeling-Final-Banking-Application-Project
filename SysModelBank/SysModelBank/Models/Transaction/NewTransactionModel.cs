using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysModelBank.Data.Models;
using SysModelBank.Models.Accounts;

namespace SysModelBank.Models.Transaction
{
    public class NewTransactionModel
    {
        public string TransactionCode { get; set; }
        public int TransactionSource { get; set; }
        public int TransactionRecipient { get; set; }
        public string TransactionDescription { get; set; } = "";
        public decimal TransactionAmount { get; set; } = 0m;
        public SelectList AvailableAccounts { get; set; }
    }
}
