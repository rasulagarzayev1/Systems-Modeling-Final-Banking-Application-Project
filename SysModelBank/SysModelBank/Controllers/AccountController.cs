using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Extensions;
using SysModelBank.Models.Identity;
using SysModelBank.Models.Settings;
using System.Threading.Tasks;
using SysModelBank.Models;
using SysModelBank.Services.Logger;

namespace SysModelBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBankLogger _logger;

        public AccountController(IAccountRepository accountRepository, IUserRepository userRepository, IBankLogger logger)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount()
        {
            var user = await _userRepository.GetAsync(User.Id());

            if (user.Accounts.Count >= 5)
            {
                return RedirectToAction("Index", "User", new NotificationModel("You cannot have more than 5 accounts!").asError());
            }

            await _accountRepository.CreateAsync(new Account
            {
                UserId = User.Id()
            });

            return RedirectToAction("Index", "User", new NotificationModel("Account creation successful!").asSuccess());
        }
    }
}
