using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Models.Settings;
using SysModelBank.Models.Accounts;
using SysModelBank.Models.Identity;
using SysModelBank.Models.Settings;

namespace SysModelBank.Extensions
{
    public static class ModelExtensions
    {
        public static AccountModel ToAccountModel(this Account account, Currency requestedCurrency) =>
            new AccountModel
            {
                Balance = account.Balance * requestedCurrency.RateFromEur,
                Id = account.Id
            };

        public static IEnumerable<AccountModel> ToAccountsListModels(this ICollection<Account> accounts, Currency requestedCurrency) =>
            accounts.Select(x => x.ToAccountModel(requestedCurrency));

        public static UserModel ToUserModel(this User user) =>
            new UserModel
            {
                Address = user.Address,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                Status = user.Status,
                Currency = new CurrencyModel
                {
                    Id = user.CurrencyId,
                    Name = user.Currency.Name
                },
                Accounts = user.Accounts.ToAccountsListModels(user.Currency)
            };
    }
}
