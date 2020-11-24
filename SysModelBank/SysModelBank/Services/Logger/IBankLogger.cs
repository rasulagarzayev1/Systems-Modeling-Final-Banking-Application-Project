
namespace SysModelBank.Services.Logger
{
    public interface IBankLogger
    {
        void Log(int typeId, string origin, string value);
    }
}
