using SysModelBank.Data.Models.Settings;

namespace SysModelBank.Data.Repositories.Settings
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
