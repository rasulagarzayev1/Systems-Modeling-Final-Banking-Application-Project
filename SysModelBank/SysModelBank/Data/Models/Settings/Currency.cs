using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysModelBank.Data.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysModelBank.Data.Models.Settings
{
    public class Currency : IBaseEntity
    {
        public const int EurId = 1;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal RateFromEur { get; set; }
    }

    public class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
