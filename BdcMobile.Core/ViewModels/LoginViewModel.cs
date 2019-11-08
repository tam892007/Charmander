using Android.Content;
using Android.Views.InputMethods;
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
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
            }
        }
        public string ErrorMessage { get; set; }
        public IMvxAsyncCommand LoginCommand { get; private set; }

        public LoginViewModel(ILoginService loginService, IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _loginService = loginService;

            //UserName = "lynt";
            //Password = "123456";
            
            if (App.User != null && !string.IsNullOrWhiteSpace(App.User.api_token))
            {
                var user = _loginService.VerifyAsync(App.User.api_token);
                if(user != null)
                {

                }
                NavigationService.Navigate<EventListViewModel>();
            } else
            {

            }
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public override async Task Initialize()
        {
            //var token = GetToken();
            UserName = await SecureStorage.GetAsync(Constants.SecureStorageKey.Username);
            Password = await SecureStorage.GetAsync(Constants.SecureStorageKey.Password);
            if (!string.IsNullOrWhiteSpace(UserName) || !string.IsNullOrWhiteSpace(Password))
            {
                IsChecked = true;
                if(!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    await Login();
                }                
            }
               
            await base.Initialize();
        }

        public async Task<string> GetToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                return oauthToken;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            return null;
        }

        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập tài khoản và mật khẩu.";
                return ;
            } else
            {
                if (IsChecked)
                {
                    try
                    {
                        await SecureStorage.SetAsync(Constants.SecureStorageKey.Username, UserName);
                        await SecureStorage.SetAsync(Constants.SecureStorageKey.Password, Password);
                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                    }
                } else
                {
                    SecureStorage.Remove(Constants.SecureStorageKey.Username);
                    SecureStorage.Remove(Constants.SecureStorageKey.Password);
                }
            }

            var user = await _loginService.LoginAsync(UserName, Password);
            if (user.IsAuthenticated)
            {
                try
                {
                    await SecureStorage.SetAsync("oauth_token", user.api_token);
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }
                await NavigationService.Navigate<EventListViewModel>();
            } else
            {
                ErrorMessage = user.ErrorMessage;
                //await NavigationService.Navigate<EventListViewModel>();
            }
        }
    }
}
