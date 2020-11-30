using SysModelBank.Data.Models;

namespace SysModelBank.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
