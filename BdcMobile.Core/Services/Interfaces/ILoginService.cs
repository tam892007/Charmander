using BdcMobile.Core.Models;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface ILoginService
    {
        User Login(string userName, string password);

        /// <summary>
        /// Verify user with token string
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        User Verify(string token);
    }
}
