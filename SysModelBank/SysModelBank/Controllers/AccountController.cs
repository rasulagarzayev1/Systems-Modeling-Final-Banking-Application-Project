using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Extensions;
using SysModelBank.Models.Identity;
using SysModelBank.Models.Settings;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        public AccountController(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetAsync(User.Id());

            return View(MapToUserModel(user));
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userRepository.GetAsync(User.Id());

            return View(MapToUserModel(user));
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(UserModel model)
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.UserName = model.Username;
            user.PhoneNumber = model.Phone;
            user.Address = model.Address;
            user.Email = model.Email;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount()
        {
             await _accountRepository.CreateAsync(new Account
            {
                UserId = User.Id()
            });

            return RedirectToAction("Index", "Overview");
        }

        [HttpPost]
        public async Task<IActionResult> SetCurrency(int currencyId)
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.CurrencyId = currencyId;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        private UserModel MapToUserModel(User user) =>
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
                }
            };
    }
}
