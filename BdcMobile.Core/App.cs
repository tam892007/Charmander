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
        public static User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        private static User _user;

        
        public override void Initialize()
        {
            RegisterDependencies();
            RegisterAppStart<LoginViewModel>();
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
