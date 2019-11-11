using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class DebugViewModel : BaseViewModel<object>
    {
        private ICloudMessaging _cloudMessaging;
        private IAppInfo _appInfo;
        public string CloudMessagingToken { get; set; }
        public string AppVersion { get; set; }
        public DebugViewModel(IMvxLogProvider mvxLogProvider, IMvxNavigationService mvxNavigationService, 
            ICloudMessaging cloudMessaging, IAppInfo appInfo) : base(mvxLogProvider, mvxNavigationService)
        {
            _cloudMessaging = cloudMessaging;
            _appInfo = appInfo;
        }

        public override async Task Initialize()
        {
            CloudMessagingToken = _cloudMessaging.GetCloudMessagingToken();
            AppVersion = _appInfo.GetAppVersion();
            await base.Initialize();
        }

        public override void Prepare(object parameter)
        {

        }
    }
}
