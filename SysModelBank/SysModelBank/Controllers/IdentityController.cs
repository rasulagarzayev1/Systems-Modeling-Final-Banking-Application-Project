using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysModelBank.Services.Logger;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Models.Identity;
using System.Threading.Tasks;
using SysModelBank.Data.Enums;
using SysModelBank.Services.Identity;
using SysModelBank.Models;

namespace SysModelBank.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBankLogger _logger;
        private readonly IUserService _userService;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IBankLogger logger, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (await _userService.LoginWithPasswordAsync(model.Username, model.Password))
            {
                return RedirectToAction("Index", "Landing");
            }

            _logger.Log("IdentityController", "User " + model.Username + " logged in");
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
                Address = model.Address,
                Status = UserStatus.PendingVerification
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                ViewBag.Notification = new NotificationModel("Account creation failed!").asError();
                return RedirectToAction("Register", "Landing");
            }

            ViewBag.Notification = new NotificationModel( "Account creation succeeded. Wait for admin verification!").asSuccess();

            _logger.Log("IdentityController", "User " + model.Username + " was created.");

             return RedirectToAction("Index", "Landing");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Landing");
        }

    }
}
