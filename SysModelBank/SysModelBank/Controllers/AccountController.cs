using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Models;
using SysModelBank.Data.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount()
        {
             await _accountRepository.CreateAsync(new Account
            {
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            });

            return RedirectToAction("Index", "Overview");
        }
    }
}
