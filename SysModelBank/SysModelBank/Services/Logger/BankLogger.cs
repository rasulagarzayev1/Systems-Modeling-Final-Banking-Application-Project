
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SysModelBank.Data;
using SysModelBank.Data.Models;
using System;
using System.Threading.Tasks;

namespace SysModelBank.Services.Logger
{
    public class BankLogger : IBankLogger
    {
        private readonly ILogger<BankLogger> _logger;
        private readonly SysModelBankDbContext DbContext;

        public BankLogger(ILogger<BankLogger> logger, SysModelBankDbContext dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        public void Log(string origin, string value)
        {
            _logger.LogInformation("Log from " + origin + " with message " + value + " at " + DateTime.Now);

            AddLogToDb(new Log { 
                origin = origin,
                value = value,
                time = DateTime.Now
            });
        }

        public void AddLogToDb(Log log)
        {
            DbContext.Add(log);

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }

                throw;
            }
        }
    }
}
