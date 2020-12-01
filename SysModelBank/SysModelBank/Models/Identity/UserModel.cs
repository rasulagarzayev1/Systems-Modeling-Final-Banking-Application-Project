using SysModelBank.Data.Enums;

namespace SysModelBank.Models.Identity
{
    public class UserModel
    {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public UserStatus Status { get; set; }
    }
}
