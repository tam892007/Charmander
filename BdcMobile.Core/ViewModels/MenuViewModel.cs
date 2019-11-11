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
        private readonly IAppContext _appContext;

        public string UserDisplayName { get; private set; }
        public string UserAvatar { get; private set; }

        public double DownsampleWidth => 200d;
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };

        public MenuViewModel(IMvxNavigationService navigationService, IAppContext appContext)
        {
            _navigationService = navigationService;
            _appContext = appContext;

            ShowSettingsCommand = new MvxAsyncCommand(async () => await System.Threading.Tasks.Task.Delay(10000));
            ShowDebugCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DebugViewModel>());
            DoLogoutCommand = new MvxAsyncCommand(async () => await DoLogOut());

            UserDisplayName = appContext.UserDisplayName;
            UserAvatar = Constants.AppAPI.IPAPI + appContext.AvatarUrl;
        }

        private async Task DoLogOut()
        {
            SecureStorage.RemoveAll();
            _appContext.Reset();
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
