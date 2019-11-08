using BdcMobile.Core.Commons;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BdcMobile.Core.ViewModels
{
    public class LoginViewModel : MvxNavigationViewModel
    {
        private readonly ILoginService _loginService;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public IMvxAsyncCommand LoginCommand { get; private set; }

        private bool _isCallingLogin;
        public bool IsCallingLogin
        {
            get => _isCallingLogin;
            private set => SetProperty(ref _isCallingLogin, value);
        }

        public LoginViewModel(ILoginService loginService, IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _loginService = loginService;

            if (App.User != null && !string.IsNullOrWhiteSpace(App.User.api_token))
            {
                var user = _loginService.VerifyAsync(App.User.api_token);
                if (user != null)
                {

                }
                NavigationService.Navigate<EventListViewModel>();
            }
            else
            {

            }

            LoginCommand = new MvxAsyncCommand(Login);
        }

        public override async Task Initialize()
        {
            UserName = await SecureStorage.GetAsync(Constants.SecureStorageKey.Username);
            Password = await SecureStorage.GetAsync(Constants.SecureStorageKey.Password);
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                LoginCommand.Execute();
            }

            await base.Initialize();
        }

        public async Task<string> GetToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync(Constants.SecureStorageKey.OAuthToken);
                return oauthToken;
            }
            catch
            {
                // Possible that device doesn't support secure storage on device.
            }

            return string.Empty;
        }

        private async Task Login()
        {
            IsCallingLogin = true;
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập tài khoản và mật khẩu.";
                return ;
            }

            var user = await _loginService.LoginAsync(UserName, Password);
            if (user.IsAuthenticated)
            {
                try
                {
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.OAuthToken, user.api_token);
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.Username, UserName);
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.Password, Password);
                }
                catch
                {
                    // Possible that device doesn't support secure storage on device.
                }

                await NavigationService.Navigate<EventListViewModel>();
            } 
            else
            {
                ErrorMessage = user.ErrorMessage;
            }

            IsCallingLogin = false;
        }

        private void OnLoginFailed(Exception exception)
        {
            // log the handled exception!
        }
    }
}
