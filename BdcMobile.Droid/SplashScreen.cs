using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace BdcMobile.Droid
{
    [Activity(
        MainLauncher = true,
        Theme = "@style/Theme.Splash",
        NoHistory = true)]
    public class SplashScreen: MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}