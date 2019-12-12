using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.ViewModels;
using Cheesebaron.MvxPlugins.Settings;
using MvvmCross.Navigation;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BdcMobile.Core.Services.Implementations
{
    public class LoginService: ILoginService
    {
        private IHttpService _httpService;
        private ISettings _settings;
        public LoginService (IHttpService httpService, ISettings settings)
        {
            _httpService = httpService;
            _settings = settings;
        }

        /// <summary>
        /// Login with username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> LoginAsync(string userName, string password, string fcmToken, CancellationToken token = default)
        {
            var user = new User
            {
                AccountName = userName,
                Password = password,
                FCMToken = fcmToken,
            };
            
            var result = await _httpService.LoginAsync(user, token);
            if (result != null)
            {
                _settings.AddOrUpdateValue(Constants.AppConfig.UserLoggedIn, true);
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

        public void LogOut()
        {
            ///Fire & Forget. Call API to delete FCM token
            Task.Run(() => { _httpService.Logout(App.Context.ApiToken); });

            ///Local data removal
            _settings.AddOrUpdateValue(Constants.AppConfig.UserLoggedIn, false);
            SecureStorage.RemoveAll();
            App.Context.Reset();
        }
    }
}
