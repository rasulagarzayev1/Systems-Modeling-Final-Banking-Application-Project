using SysModelBank.Data.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysModelBank.Data.Models
{
    public class Account : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0m;

        public virtual User User { get; set; }

        public virtual ICollection<Transaction> SentTransactions { get; set; }

        public virtual ICollection<Transaction> RecievedTransactions { get; set; }
    }
}
