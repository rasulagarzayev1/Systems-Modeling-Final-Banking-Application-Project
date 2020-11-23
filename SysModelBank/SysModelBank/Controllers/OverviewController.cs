using Microsoft.AspNetCore.Mvc;
using SysModelBank.Data.Models.Identity;

namespace SysModelBank.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
