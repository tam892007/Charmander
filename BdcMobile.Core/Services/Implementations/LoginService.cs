using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;

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
        public User Login(string userName, string password)
        {
            var user = new User
            {
                AccountName = userName,
                Password = password
            };
            if (string.IsNullOrWhiteSpace(user.AccountName) || string.IsNullOrWhiteSpace(user.Password))
            {
                user.ErrorMessage = "Vui lòng nhập tài khoản và mật khẩu.";
                return user;
            }
            var result = _httpService.LoginAsync(user);
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
        public User Verify(string token)
        {
            var user = _httpService.VerifyUserAsync(token);
            return user;
        }
    }
}
