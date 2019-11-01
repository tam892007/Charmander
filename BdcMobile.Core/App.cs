using BdcMobile.Core.Services.Implementations;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BdcMobile.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            RegisterDependencies();
            RegisterAppStart<LoginViewModel>();
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterType<ILoginService, LoginService>();
        }
    }
}
