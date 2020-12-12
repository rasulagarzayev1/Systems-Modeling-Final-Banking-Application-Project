using SysModelBank.Data.Models;
using System.Collections.Generic;

namespace SysModelBank.Areas.Admin.Models.LogOverview
{
    public class LogList
    {
        public IEnumerable<Log> Logs { get; set; }
    }
}
