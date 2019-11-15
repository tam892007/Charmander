using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Implementations
{
    public class LoginService: ILoginService
    {
        private IHttpService _httpService;

        public LoginService (IHttpService httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// Login with username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> LoginAsync(string userName, string password, CancellationToken token = default)
        {
            var user = new User
            {
                AccountName = userName,
                Password = password
            };
            
            var result = await _httpService.LoginAsync(user, token);
            if (result != null)
            {
                result.IsAuthenticated = true;
                App.User = result;
                return result;
            } else
            {
                user.IsAuthenticated = false;
                return user;
            }
        }


        /// <summary>
        /// Verify user with token string
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<User> VerifyAsync(string token)
        {
            var user = await _httpService.VerifyUserAsync(token);
            return user;
        }
    }
}
