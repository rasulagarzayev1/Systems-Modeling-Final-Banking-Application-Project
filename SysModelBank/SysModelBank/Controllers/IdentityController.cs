using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysModelBank.Services.Logger;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Models.Identity;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBankLogger _logger;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IBankLogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Landing");
            }

            _logger.Log(0, "IdentityController", "User " + model.Username + " logged in");
            return RedirectToAction("Index", "Overview");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return RedirectToAction("Register", "Landing");
            }

            await _signInManager.SignInAsync(user, true);

            _logger.Log(5, "IdentityController", "User " + model.Username + " was registered.");
            return RedirectToAction("Index", "Overview");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Landing");
        }

    }
}
