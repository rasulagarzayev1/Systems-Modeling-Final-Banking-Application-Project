using System.Collections.Generic;

namespace SysModelBank.Areas.Admin.Models.TransactionOverview
{
    public class TransactionList
    {
        public IEnumerable<TransactionListItem> PendingUndoing { get; set; }

        public IEnumerable<TransactionListItem> Transactions { get; set; }
    }
}
