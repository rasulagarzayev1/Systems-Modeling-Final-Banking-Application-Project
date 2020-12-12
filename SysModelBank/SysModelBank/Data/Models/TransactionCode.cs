using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data.Models
{
    public class TransactionCode : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int RecipientId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public virtual Account RecipientAccount { get; set; }

    }

    public class TransactionCOdeEntityTypeConfiguration : IEntityTypeConfiguration<TransactionCode>
    {
        public void Configure(EntityTypeBuilder<TransactionCode> builder)
        {
            builder.HasOne(x => x.RecipientAccount).WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
