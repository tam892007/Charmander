using BdcMobile.Core.Commons;
using BdcMobile.Core.Interactions;
using BdcMobile.Core.Services.Interfaces;
using Cheesebaron.MvxPlugins.Settings;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class SettingsViewModel : BaseViewModel<object>
    {
        private ISettings _settings;
        private ILoginService _loginService;
        private ICommonService _commonService;
        private MvxInteraction<YesNoQuestion> _confirmSave;

        public IMvxInteraction<YesNoQuestion> ConfirmSave => _confirmSave;

        public IMvxCommand SaveSettingsCommand { get; private set; }
        public string ServerAddress { get;set; }
        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettings settings, ILoginService loginService, ICommonService commonService) 
            : base(logProvider, navigationService)
        {
            _loginService = loginService;
            _settings = settings;
            _commonService = commonService;
            _confirmSave = new MvxInteraction<YesNoQuestion>();
            SaveSettingsCommand = new MvxCommand(AskToProceed);
        }

        public override async Task Initialize()
        {
            ServerAddress = App.Context.ServerAddress;
            await base.Initialize();
        }

        public override void Prepare(object parameter)
        {
        }

        public void AskToProceed()
        {
            if (App.Context.IsUserAuthenticated)
            {
                var request = new YesNoQuestion
                {
                    YesNoCallback = async (ok) =>
                    {
                        if (ok)
                        {
                            await SaveSettingAndLogOut();
                        }
                    },
                    Question = "Ứng dụng sẽ đăng xuất bạn để lưu sự thay đổi. Bạn có muốn tiếp tục?"
                };

                _confirmSave.Raise(request);
            }
            else
            {
                SaveSetting();
                _commonService.ShowToast("Lưu thành công.");
            }
        }

        public async Task SaveSettingAndLogOut()
        {
            SaveSetting();
            _loginService.LogOut();
            await NavigationService.Navigate<LoginViewModel>();
        }

        public void SaveSetting()
        {
            _settings.AddOrUpdateValue(Constants.AppConfig.ServerAddressKey, ServerAddress);
            App.Context.SetServerAddress(ServerAddress);
        }

        public override async Task BackCommandTask()
        {
            if (App.Context.IsUserAuthenticated)
            {
                await base.BackCommandTask();
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}
