using BdcMobile.Core.Models;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface ILoginService
    {
        User Login(string userName, string password);
    }
}
