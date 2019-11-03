using Android.App;
using Android.OS;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Login")]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }
    }
}