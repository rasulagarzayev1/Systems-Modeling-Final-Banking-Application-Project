using SysModelBank.Data.Models;

namespace SysModelBank.Data.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
