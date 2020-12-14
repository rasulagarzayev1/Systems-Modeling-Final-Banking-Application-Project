using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysModelBank.Areas.Admin.Models.UserManagement;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories.Identity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SysModelBank.Services.Logger;
using Microsoft.AspNetCore.Authorization;

namespace SysModelBank.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBankLogger _logger;
        private readonly UserManager<User> _userManager;

        public UserManagementController(IUserRepository userRepository, IBankLogger logger, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAsync();

            var pending = new List<UserListItem>();
            var deleting = new List<UserListItem>();
            var activeUsers = new List<UserListItem>();

            foreach (var user in users)
            {
                if (user.Status == UserStatus.Active)
                {
                    activeUsers.Add(await MapToListItem(user));
                }
                else if (user.Status == UserStatus.PendingVerification)
                {
                    pending.Add(await MapToListItem(user));
                }
                else if (user.Status == UserStatus.PendingDeletion)
                {
                    deleting.Add(await MapToListItem(user));
                }
            }

            return View(new UserList
            {
                PendingUsers = pending,
                DeletingUsers = deleting,
                Users = activeUsers
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var user = await _userRepository.GetAsync(id);

            return View(await MapToDetail(user, currentUser));
        }

        [HttpPost]
        public async Task<IActionResult> Verify(int id)
        {
            if (!User.IsInRole(Role.Admin))
            {
                return RedirectToAction("Details", new { id });
            }

            var user = await _userRepository.GetAsync(id);

            user.Status = UserStatus.Active;

            await _userRepository.UpdateAsync(user);
            _logger.Log("UserManagementController", $"Admin {HttpContext.User.Identity.Name} verified account {user.UserName}");

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole(Role.Admin))
            {
                return RedirectToAction("Details", new { id });
            }


            var user = await _userRepository.GetAsync(id);

            user.Status = UserStatus.Deleted;

            await _userRepository.UpdateAsync(user);
            _logger.Log("UserManagementController", $"Admin {HttpContext.User.Identity.Name} deleted account {user.UserName}");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var user = await _userRepository.GetAsync(id);

            return View(await MapToDetail(user, currentUser));
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(int id, UserDetail model)
        {
            var user = await _userRepository.GetAsync(id);

            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.UserName = model.Username;
            user.PhoneNumber = model.Phone;
            user.Address = model.Address;
            user.Email = model.Email;

            await _userRepository.UpdateAsync(user);
            _logger.Log("UserManagementController", $"Admin {HttpContext.User.Identity.Name} changed contact information for {user.UserName}");

            return RedirectToAction("Details", new { id });
        }

        private async Task<UserListItem> MapToListItem(User user)
        {
            return new UserListItem
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                AccountCount = user.Accounts.Count()
            };
        }

        private async Task<UserDetail> MapToDetail(User user, User currentUser)
        {
            return new UserDetail
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.UserName,
                Address = user.Address,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Status = user.Status,
                Accounts = user.Accounts.Select(x => new UserAccountModel { Balance = Math.Round(x.Balance * currentUser.Currency.RateFromEur, 2), Id = x.Id, Currency = currentUser.Currency.Name})
            };
        }
    }
}
