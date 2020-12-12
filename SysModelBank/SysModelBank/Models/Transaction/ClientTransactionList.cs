using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysModelBank.Areas.Admin.Models.TransactionOverview;
using SysModelBank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysModelBank.Models.Transaction
{
    public class ClientTransactionList
    {
        [BindProperty]
        public int SelectedAccountId { get; set; }

        public string CurrencyLocalization { get; set; }
        public IEnumerable<SelectListItem> Accounts { get; set; }

        public IEnumerable<TransactionListItem> Transactions { get; set; }
    }
}
