using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

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
            LoginCommand = new MvxAsyncCommand(Login);
        }

        private async Task Login()
        {
            var user = _loginService.Login(UserName, Password);
            if (user.IsAuthenticated)
            {
                await NavigationService.Navigate<EventListViewModel>();
            }
        }
    }
}
