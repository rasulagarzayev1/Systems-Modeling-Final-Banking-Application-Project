using Microsoft.AspNetCore.Identity;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using System.Threading.Tasks;

namespace SysModelBank.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> LoginWithPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.Status != UserStatus.Active)
            {
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            return result.Succeeded;
        }

    }
}
