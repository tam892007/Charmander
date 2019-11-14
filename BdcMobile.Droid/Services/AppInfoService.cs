using BdcMobile.Core.Services.Interfaces;

namespace BdcMobile.Droid.Services
{
    class AppInfoService : IAppInfo
    {
        public string GetAppVersion()
        {
            var ctx = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.ApplicationContext;
            return ctx.PackageManager.GetPackageInfo(ctx.PackageName, 0).VersionName;
        }
    }
}