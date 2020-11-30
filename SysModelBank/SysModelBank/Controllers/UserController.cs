using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories.Identity;
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
        public async Task<IActionResult> Verify(int userId)
        {
            if (!User.IsInRole(Role.Admin))
            {
                return RedirectToAction("Index", "Overview");
            }

            var user = await _userRepository.GetAsync(userId);

            user.Status = UserStatus.Active;

            await _userRepository.UpdateAsync(user);

            return RedirectToAction("Index", "Overview");
        }
    }
}
