using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Extensions;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RequestDelete()
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.Status = UserStatus.PendingDeletion;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CancelDelete()
        {
            var user = await _userRepository.GetAsync(User.Id());

            user.Status = UserStatus.Active;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index", "Account");
        }
    }
}
