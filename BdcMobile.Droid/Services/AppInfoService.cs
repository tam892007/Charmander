using Android.Support.Constraints;
using BdcMobile.Core.Services.Interfaces;

namespace BdcMobile.Droid.Services
{
    class AppInfoService : IAppInfo
    {
        public string GetAppVersion()
        {
            return BuildConfig.VersionName;
        }
    }
}