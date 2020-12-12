using Microsoft.AspNetCore.Mvc;
using SysModelBank.Areas.Admin.Models.LogOverview;
using SysModelBank.Data.Repositories;
using System.Threading.Tasks;

namespace SysModelBank.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogOverviewController : Controller
    {
        private readonly ILogRepository _logRepository;

        public LogOverviewController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _logRepository.GetAsync();

            return View(new LogList
            {
                Logs = logs
            });
        }
    }
}
