using Android.App;
using Android.OS;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.UIListenner;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Notification List")]
    public class NotificationListView : MvxAppCompatActivity<NotificationListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.NotificationList);
        }
    }
}