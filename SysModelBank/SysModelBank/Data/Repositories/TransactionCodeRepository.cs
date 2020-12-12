using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models;

namespace SysModelBank.Data.Repositories
{
    public class TransactionCodeRepository : Repository<TransactionCode>, ITransactionCodeRepository
    {
        public TransactionCodeRepository(SysModelBankDbContext dbContext) : base(dbContext)
        {
        }

        public Task<TransactionCode> GetAsync(string code)
        {
            return Query().SingleOrDefaultAsync(x => x.Code == code);
        }
    }
}
