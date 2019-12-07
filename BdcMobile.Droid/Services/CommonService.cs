using Android.Content;
using Android.Preferences;
using Android.Widget;
using BdcMobile.Core.Services.Interfaces;
using System;

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

        public static T GetFromSharedPreferences<T>(Context context, string key)
        {
            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(context);

            var t = typeof(T);
            if (t == typeof(int))
            {
                return (T)Convert.ChangeType(sharedPreferences.GetInt(key, 0), typeof(T));
            }
            else if (t == typeof(bool))
            {
                return (T)Convert.ChangeType(sharedPreferences.GetBoolean(key, false), typeof(T));
            }

            return (T)Convert.ChangeType(sharedPreferences.GetString(key, string.Empty), typeof(T));
        }

        public static void PutToSharedPreferences<T>(Context context, string key, object value)
        {
            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(context);
            var t = typeof(T);
            if (t == typeof(int))
            {
                sharedPreferences.Edit().PutInt(key, (int)value).Apply();
            }
            else
            {
                sharedPreferences.Edit().PutString(key, (string)value).Apply();
            }
        }
    }
}