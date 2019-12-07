using BdcMobile.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<User> LoginAsync(string userName, string password, string fcmToken, CancellationToken token);

        /// <summary>
        /// Verify user with token string
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> VerifyAsync(string token);

        void LogOut();
    }
}
