using Android.Views;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Droid.Bindings;
using BdcMobile.Droid.CloudMessaging;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace BdcMobile.Core
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterType<ICloudMessaging>(() => new FirebaseCloudMessaging());
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterPropertyInfoBindingFactory(
                typeof(DroidVisibilityBinding),
                typeof(View), "Visibility");
        }
    }
}
