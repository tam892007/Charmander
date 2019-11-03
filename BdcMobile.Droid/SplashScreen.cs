using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace BdcMobile.Droid
{
    [Activity(
        MainLauncher = true,
        Theme = "@style/Theme.Splash",
        NoHistory = true)]
    public class SplashScreen: MvxSplashScreenAppCompatActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}