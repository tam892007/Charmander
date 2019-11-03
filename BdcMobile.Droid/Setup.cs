using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Droid;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace BdcMobile.Core
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton<ICloudMessaging>(new FirebaseCloudMessaging(ApplicationContext));
        }
    }
}
