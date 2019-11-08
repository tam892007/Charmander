using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Implementations;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BdcMobile.Core
{
    public class App: MvxApplication
    {
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
        }

        private void RegisterDependencies()
        {
            //Mvx.IoCProvider.RegisterSingleton<, LoginService>();
            Mvx.IoCProvider.RegisterType<ILoginService, LoginService>();
            Mvx.IoCProvider.RegisterType<IHttpService, HttpService>();
            Mvx.IoCProvider.RegisterType<IEventService, EventService>();
        }
    }
}
