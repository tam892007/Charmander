﻿using BdcMobile.Core.Services.Interfaces;
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

        public override Task Initialize()
        {
            //var token = GetToken();
            return base.Initialize();
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
                await NavigationService.Navigate<EventListViewModel>();
            }
        }
    }
}
