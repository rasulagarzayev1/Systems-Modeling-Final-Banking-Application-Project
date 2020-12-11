using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Models.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysModelBank.Data.Models
{
    public class Transaction : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Done;

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public int CreatorUserId { get; set; }

        [Required]
        public int SenderAccountId { get; set; }

        [Required]
        public int RecipientAccountId { get; set; }

        public virtual User CreatorUser { get; set; }

        public virtual Account SenderAccount { get; set; }

        public virtual Account RecipientAccount { get; set; }
    }

    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(x => x.CreatorUser).WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SenderAccount).WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RecipientAccount).WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
