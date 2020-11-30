using System.Threading.Tasks;

namespace SysModelBank.Services.Identity
{
    public interface IUserService
    {
        Task<bool> LoginWithPasswordAsync(string username, string password);
    }
}
