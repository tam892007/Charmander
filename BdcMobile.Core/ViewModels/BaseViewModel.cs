using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;

        public BaseViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _loginService = loginService;
            _navigationService = navigationService;

            if (App.User != null && !string.IsNullOrWhiteSpace(App.User.api_token))
            {
                var user = _loginService.Verify(App.User.api_token);
                if (user != null)
                {

                }
                _navigationService.Navigate<EventListViewModel>();
            } else
            {
                _navigationService.Navigate<LoginViewModel>();
            }

        }

    }
}
