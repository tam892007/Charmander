using Android.App;
using Android.Content.PM;
using Android.OS;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Login", LaunchMode = LaunchMode.SingleTask, Theme = "@style/Theme.Splash")]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.SetTheme(Resource.Style.AppTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }
    }
}