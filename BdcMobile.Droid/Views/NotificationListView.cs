using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.Extensions;
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

            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.lstNotification);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                recyclerView.SetLayoutManager(layoutManager);
                recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.LoadMoreTask, () => ViewModel.LoadMoreCommand);
            }
        }
    }
}