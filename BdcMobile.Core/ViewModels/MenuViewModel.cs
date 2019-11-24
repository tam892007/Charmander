using BdcMobile.Core.Commons;
using BdcMobile.Core.Services.Interfaces;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BdcMobile.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;

        public string UserDisplayName { get; private set; }
        public string UserAvatar { get; private set; }

        public double DownsampleWidth => 200d;
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };

        public MenuViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;

            ShowSettingsCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<SettingsViewModel>());
            ShowDebugCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DebugViewModel>());
            DoLogoutCommand = new MvxAsyncCommand(async () => await DoLogOut());

            UserDisplayName = App.Context.UserDisplayName;
            UserAvatar = App.Context.ServerAddress + App.Context.AvatarUrl;
        }

        private async Task DoLogOut()
        {
            _loginService.LogOut();
            await _navigationService.Navigate<LoginViewModel>();
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands
        public IMvxCommand ShowSettingsCommand { get; private set; }
        public IMvxCommand ShowDebugCommand { get; private set; }
        public IMvxAsyncCommand DoLogoutCommand { get; private set; }

        // Private methods
    }
}
