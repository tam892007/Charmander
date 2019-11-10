using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowSettingsCommand = new MvxAsyncCommand(async () => await System.Threading.Tasks.Task.Delay(10000));
            ShowDebugCommand = new MvxAsyncCommand(async () => await System.Threading.Tasks.Task.Delay(10000));
            DoLogoutCommand = new MvxAsyncCommand(async () => await System.Threading.Tasks.Task.Delay(10000));
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands
        public IMvxCommand ShowSettingsCommand { get; private set; }
        public IMvxCommand ShowDebugCommand { get; private set; }
        public IMvxCommand DoLogoutCommand { get; private set; }

        // Private methods
    }
}
