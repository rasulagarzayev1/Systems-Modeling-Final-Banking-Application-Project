using System.Threading.Tasks;

namespace SysModelBank.Services.Identity
{
    public interface IUserService
    {
        Task<(bool, string)> LoginWithPasswordAsync(string username, string password);
    }
}
