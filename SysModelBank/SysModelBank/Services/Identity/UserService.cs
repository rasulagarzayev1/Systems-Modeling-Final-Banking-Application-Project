using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models.Identity;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task<(bool, string)> LoginWithPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return (false, "No account with these details was found!");
            }
            Debug.WriteLine(user.Status);
            if (user.Status != UserStatus.Active && user.Status != UserStatus.PendingDeletion)
            {
                return (false, "This account hasn't been activated yet!");
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            return (result.Succeeded, "");
        }

    }
}
