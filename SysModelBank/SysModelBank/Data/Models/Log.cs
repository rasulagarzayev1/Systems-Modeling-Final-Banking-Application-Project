using System;
using System.ComponentModel.DataAnnotations;

namespace SysModelBank.Data.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string origin { get; set; }

        [Required]
        public string value { get; set; }

        [Required]
        public DateTime time { get; set; }
    }
}
