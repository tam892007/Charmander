using Android.App;
using Android.Content;
using Android.OS;
using BdcMobile.Core.Commons;
using BdcMobile.Core.ViewModels;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BdcMobile.Droid
{
    [Activity(
        MainLauncher = true,
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    [MvxActivityPresentation]
    public class SplashScreen: MvxSplashScreenAppCompatActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            HandleIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            HandleIntent(intent);
        }

        protected async void HandleIntent(Intent intent)
        {
            if (!string.IsNullOrEmpty(intent.GetStringExtra(Constants.AppConfig.FCMExtraName)))
            {
                if (Mvx.IoCProvider.TryResolve(out IMvxNavigationService mvxNavigationService))
                {
                    var notification = JsonConvert.DeserializeObject<Core.Models.Notification>(intent.GetStringExtra(Constants.AppConfig.FCMExtraName));
                    switch (notification.Type)
                    {
                        case Core.Models.NotificationType.NewChat:
                            await mvxNavigationService.Navigate(typeof(EventDetailsViewModel), new BdcMobile.Core.Models.Event { SurveyID = notification.SurveyID });
                            break;
                        default: break;
                    }
                }
            }
        }

        protected override async Task RunAppStartAsync(Bundle bundle)
        {
            if (MvvmCross.Mvx.IoCProvider.TryResolve(out IMvxAppStart startup))
            {
                Core.Models.Notification notification = null;
                if (!string.IsNullOrEmpty(Intent.GetStringExtra(Constants.AppConfig.FCMExtraName)))
                {
                    notification = JsonConvert.DeserializeObject<Core.Models.Notification>(Intent.GetStringExtra(Constants.AppConfig.FCMExtraName));
                }

                await startup.StartAsync(notification);
                if (!startup.IsStarted)
                {
                    await startup.StartAsync(notification);
                }
                else
                {
                    Finish();
                }
            }
        }
    }
}