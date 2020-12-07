using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models.Settings;

namespace SysModelBank.Data.DataSeeders.Settings
{
    public class CurrencyDataSeeder
    {
        public void Add(ModelBuilder builder)
        {
            builder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    Name = "EUR",
                    Symbol = "€",
                    RateFromEur = 1m
                },
                new Currency
                {
                    Id = 2,
                    Name = "USD",
                    Symbol = "$",
                    RateFromEur = 1.21307m
                },
                new Currency
                {
                    Id = 3,
                    Name = "NGN",
                    Symbol = "₦",
                    RateFromEur = 461.643m
                }
            );
        }
    }
}
