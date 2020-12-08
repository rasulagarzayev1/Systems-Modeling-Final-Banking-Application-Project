using SysModelBank.Data.Repositories.Settings;
using SysModelBank.Models.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SysModelBank.Services.Settings
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<CurrencyModel>> GetAllAsync()
        {
            var currencies = await _currencyRepository.GetAsync();

            return currencies.Select(x => new CurrencyModel 
            { 
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
