using SysModelBank.Data.Models;

namespace SysModelBank.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
