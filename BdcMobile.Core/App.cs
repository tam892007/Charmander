using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Implementations;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.ViewModels;
using Cheesebaron.MvxPlugins.Settings;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BdcMobile.Core
{
    public class App: MvxApplication
    {
        public static IAppContext Context { get; private set; }
        private static User _user;
        public static User User
        {
            get
            {
                if (_user == null) _user = new User();
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        
        public override void Initialize()
        {
            base.Initialize();
            RegisterDependencies();
            RegisterCustomAppStart<AppStart>();
            Mvx.IoCProvider.Resolve<ICloudMessaging>().Initialize();
            SetContext();
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterType<ILoginService, LoginService>();
            Mvx.IoCProvider.RegisterType<IHttpService, HttpService>();
            Mvx.IoCProvider.RegisterType<IEventService, EventService>();
            Mvx.IoCProvider.RegisterSingleton<IAppContext>(new AppContext());
        }

        private void SetContext()
        {
            var settings = Mvx.IoCProvider.Resolve<ISettings>();
            var serverAddress = settings.GetValue<string>(Constants.AppConfig.ServerAddressKey, Constants.AppAPI.IPAPI);
            var pullMessageTime = settings.GetValue<int>(Constants.AppConfig.PullMessageTimeKey, Constants.AppConfig.PullMessageTime);
            Context = Mvx.IoCProvider.Resolve<IAppContext>();
            Context.SetServerAddress(serverAddress);
            Context.SetPullMessageTime(pullMessageTime);
        }
    }
}
