
using Android.App;
using Android.Content.PM;
using Android.OS;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Debug", LaunchMode = LaunchMode.SingleTop)]
    [MvxActivityPresentation]
    public class DebugView : MvxAppCompatActivity<DebugViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DebugView);
        }
    }
}