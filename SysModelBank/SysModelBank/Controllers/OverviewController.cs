using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Repositories;
using SysModelBank.Models.Overview;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public OverviewController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountRepository.GetAsync();

            return View(new OverviewModel
                {
                    UserAccounts = accounts
                });
        }
    }
}
