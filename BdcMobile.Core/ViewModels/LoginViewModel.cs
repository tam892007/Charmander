using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;
        public string UserName { get; set; }
        public string Password { get; set; }
        public IMvxAsyncCommand LoginCommand { get; private set; }

        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _loginService = loginService;
            _navigationService = navigationService;

            LoginCommand = new MvxAsyncCommand(Login);
        }

        private async Task Login()
        {
            var user = _loginService.Login(UserName, Password);
            if (user.IsAuthenticated)
            {
                await _navigationService.Navigate<EventListViewModel>();
            }
        }
    }
}
