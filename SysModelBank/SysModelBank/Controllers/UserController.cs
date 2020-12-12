using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Extensions;
using System.Threading.Tasks;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Models.Identity;
using SysModelBank.Models.Settings;
using SysModelBank.Services.Logger;

namespace SysModelBank.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBankLogger _logger;

        public UserController(IUserRepository userRepository, IBankLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetAsync(User.Id());

            return View(user.ToUserModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userRepository.GetAsync(User.Id());

            return View(user.ToUserModel());
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
        public async Task<IActionResult> SetCurrency(int currencyId)
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.CurrencyId = currencyId;

            await _userRepository.UpdateAsync(user);
            _logger.Log("AccountController", $"User currency set to {currencyId}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RequestDelete()
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.Status = UserStatus.PendingDeletion;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelDelete()
        {
            var user = await _userRepository.GetAsync(User.Id());

            if (user.Status != UserStatus.PendingDeletion) return RedirectToAction("Index");

            user.Status = UserStatus.Active;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index");
        }
    }
}
