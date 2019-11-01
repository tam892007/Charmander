using Android.App;
using Android.OS;
using BdcMobile.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Login")]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }
    }
}