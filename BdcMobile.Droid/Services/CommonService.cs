using Android.Content;
using Android.Widget;
using BdcMobile.Core.Services.Interfaces;

namespace BdcMobile.Droid.Services
{
    public class CommonService : ICommonService
    {
        public void OpenBrowser(string url)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(browserIntent);
        }

        public void ShowToast(string text)
        {
            var toast = Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext, text, ToastLength.Short);
            toast.SetGravity(Android.Views.GravityFlags.Center, 0, 0);
            toast.Show();
        }
    }
}