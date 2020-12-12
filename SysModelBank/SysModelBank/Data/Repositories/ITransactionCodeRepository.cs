using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysModelBank.Data.Models;

namespace SysModelBank.Data.Repositories
{
    public interface ITransactionCodeRepository : IRepository<TransactionCode>
    {
        public Task<TransactionCode> GetAsync(string code);
    }
}
