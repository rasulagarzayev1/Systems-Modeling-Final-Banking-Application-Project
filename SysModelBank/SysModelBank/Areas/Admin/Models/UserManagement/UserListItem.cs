namespace SysModelBank.Areas.Admin.Models.UserManagement
{
    public class UserListItem
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Role { get; set; }

        public int AccountCount { get; set; }
    }
}
