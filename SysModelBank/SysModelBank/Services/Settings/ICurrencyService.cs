using SysModelBank.Models.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysModelBank.Services.Settings
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyModel>> GetAllAsync();
    }
}