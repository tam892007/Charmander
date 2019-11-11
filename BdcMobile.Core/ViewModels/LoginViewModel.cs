using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
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
    public class LoginViewModel : BaseViewModel<Notification>
    {
        private Notification _notification; 
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
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public override async Task Initialize()
        {
            ///Auto login if not authenticated yet
            if (!AppContext.IsUserAuthenticated)
            {
                UserName = await SecureStorage.GetAsync(Constants.SecureStorageKey.Username);
                Password = await SecureStorage.GetAsync(Constants.SecureStorageKey.Password);
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    LoginCommand.Execute();
                }
            }

            await base.Initialize();
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
                    SyncContextFromUser(user);
                }
                catch
                {
                    // Possible that device doesn't support secure storage on device.
                }

                if (_notification == null)
                {
                    await NavigationService.Navigate<EventListViewModel>();
                }
                else
                {
                    await NavigationService.Navigate<EventListViewModel, Event>(new Event { SurveyID = _notification.SurveyID });
                    _notification = null;
                }
            } 
            else
            {
                ErrorMessage = user.ErrorMessage;
            }

            IsCallingLogin = false;
        }

        public override void Prepare(Notification notification)
        {
            _notification = notification;
        }
    }
}
