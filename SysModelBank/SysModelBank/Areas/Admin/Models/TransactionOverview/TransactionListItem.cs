using System;

namespace SysModelBank.Areas.Admin.Models.TransactionOverview
{
    public class TransactionListItem
    {
        public int Id { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }
}
