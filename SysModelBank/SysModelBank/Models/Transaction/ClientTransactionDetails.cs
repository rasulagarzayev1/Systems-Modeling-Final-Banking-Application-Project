using SysModelBank.Data.Enums;
using System;

namespace SysModelBank.Models.Transaction
{
    public class ClientTransactionDetails
    {
        public int Id { get; set; }

        public TransactionStatus Status { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreationTime { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public string Description { get; set; }
    }
}
