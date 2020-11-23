using System.ComponentModel.DataAnnotations;

namespace SysModelBank.Models.Identity
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
