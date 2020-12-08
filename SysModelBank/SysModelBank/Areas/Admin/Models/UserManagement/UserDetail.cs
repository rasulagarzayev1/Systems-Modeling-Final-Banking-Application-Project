using SysModelBank.Data.Enums;

namespace SysModelBank.Areas.Admin.Models.UserManagement
{
    public class UserDetail
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public UserStatus Status { get; set; }
    }
}
