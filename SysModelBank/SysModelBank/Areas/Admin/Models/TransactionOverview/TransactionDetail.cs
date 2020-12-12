using SysModelBank.Data.Enums;
using System;

namespace SysModelBank.Areas.Admin.Models.TransactionOverview
{
    public class TransactionDetail
    {
        public int Id { get; set; }

        public TransactionStatus Status { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreationTime { get; set; }

        public int SenderAccountId { get; set; }

        public string SenderName { get; set; }

        public int RecipientAccountId { get; set; }

        public string RecipientName { get; set; }

        public string Description { get; set; }

    }
}
