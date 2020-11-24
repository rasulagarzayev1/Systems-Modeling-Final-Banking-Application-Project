
using Microsoft.Extensions.Logging;
using System;

namespace SysModelBank.Services.Logger
{
    public class BankLogger : IBankLogger
    {
        private readonly ILogger<BankLogger> _logger;

        public BankLogger(ILogger<BankLogger> logger)
        {
            _logger = logger;
        }

        public void Log(int typeId, string origin, string value)
        {
            _logger.LogInformation("Thing happened with id: " + typeId + " from " + origin + " with message " + 
                value + " at " + DateTime.Now);

            // TODO: Save it to the database
        }
    }
}
