using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Droid.CloudMessaging;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Plugin.Messenger;

namespace BdcMobile.Core
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterType<ICloudMessaging>(() => new FirebaseCloudMessaging());
        }
    }
}
