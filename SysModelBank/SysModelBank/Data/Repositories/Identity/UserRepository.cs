using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Data.Repositories.Identity
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
