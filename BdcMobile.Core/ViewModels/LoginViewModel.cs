using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using Cheesebaron.MvxPlugins.Settings;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BdcMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel<Notification>
    {
        private Notification _notification; 
        private readonly ILoginService _loginService;
        private readonly ICloudMessaging _cloudMessaging;
        public string UserName { get; set; }
        public string Password { get; set; }

        private string _errorMessaage;
        public string ErrorMessage
        {
            get => _errorMessaage;
            private set => SetProperty(ref _errorMessaage, value);
        }
        public IMvxAsyncCommand LoginCommand { get; private set; }

        public IMvxAsyncCommand OpenSettingsCommand { get; private set; }

        private CancellationTokenSource CancelLoginToken;
        public IMvxCommand CancelLoginCommand { get; private set; }

        private bool _isCallingLogin;
        public bool IsCallingLogin
        {
            get => _isCallingLogin;
            private set => SetProperty(ref _isCallingLogin, value);
        }

        public LoginViewModel(ILoginService loginService, ICloudMessaging cloudMessaging, IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _loginService = loginService;
            _cloudMessaging = cloudMessaging;
            LoginCommand = new MvxAsyncCommand(Login);
            CancelLoginCommand = new MvxCommand(() => { CancelLoginToken?.Cancel(); });
            OpenSettingsCommand = new MvxAsyncCommand(async () => { await NavigationService.Navigate<SettingsViewModel>(); });
        }

        public override async Task Initialize()
        {
            ///Auto login if not authenticated yet
            if (!App.Context.IsUserAuthenticated)
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
            if (!Validate())
            {
                return;
            }

            IsCallingLogin = true;
            CancelLoginToken = new CancellationTokenSource();
            var user = await _loginService.LoginAsync(UserName, Password, _cloudMessaging.GetCloudMessagingToken(), CancelLoginToken.Token);
            if (user.IsAuthenticated)
            {
                try
                {
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.OAuthToken, user.api_token);
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.Username, UserName);
                    await SecureStorage.SetAsync(Constants.SecureStorageKey.Password, Password);
                    App.Context.SyncContextFromUser(user);
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
                    int tabIndex = 0;
                    if (_notification.Type == NotificationType.ExternalChatUpdate)
                    {
                        tabIndex = 1;
                    }

                    await NavigationService.Navigate<EventListViewModel, Event>(new Event { SurveyID = _notification.Object, TabIndex = tabIndex });
                    _notification = null;
                }
            } 
            else
            {
                ///NOT user cancel action. 
                if (!CancelLoginToken.IsCancellationRequested)
                {
                    ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? "Đăng nhập thất bại." : ErrorMessage;
                }
            }

            IsCallingLogin = false;
        }

        public override bool Validate()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = "Vui lòng nhập tài khoản.";
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập mật khẩu.";
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return false;
            }

            return true;
        }

        public override void Prepare(Notification notification)
        {
            _notification = notification;
        }

        public override void Prepare()
        { 
        }
    }
}
