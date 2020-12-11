using System.Linq;
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!await _userService.LoginWithPasswordAsync(model.Username, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return View("Index");
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
                Status = UserStatus.PendingVerification,
                Firstname = model.Firstname,
                Lastname = model.Lastname
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.ElementAtOrDefault(0);
                var errorMessage = "Account creation failed!";
                if (error != null)
                    errorMessage = error.Description;
                ViewBag.Notification = new NotificationModel(errorMessage).asError();
                return View();
            }

            ViewBag.Notification = new NotificationModel( "Account creation succeeded. Wait for admin verification!").asSuccess();

            _logger.Log("IdentityController", "User " + model.Username + " was created.");

             return RedirectToAction("Index", "Landing");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

    }
}
