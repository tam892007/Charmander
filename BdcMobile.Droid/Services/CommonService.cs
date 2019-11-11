using Android.Content;
using BdcMobile.Core.Services.Interfaces;
using System;

namespace BdcMobile.Droid.Services
{
    class CommonService : ICommonService
    {
        void ICommonService.OpenBrowser(string url)
        {
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(browserIntent);
        }
    }
}