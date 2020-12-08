using System.Collections.Generic;

namespace SysModelBank.Areas.Admin.Models.UserManagement
{
    public class UserList
    {
        public IEnumerable<UserListItem> PendingUsers { get; set; }

        public IEnumerable<UserListItem> DeletingUsers { get; set; }

        public IEnumerable<UserListItem> Users { get; set; }
    }
}
