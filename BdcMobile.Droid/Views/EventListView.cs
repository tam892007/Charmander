using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.Extensions;
using BdcMobile.Droid.UIListenner;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event List")]
    public class EventListView : MvxAppCompatActivity<EventListViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EventList);

            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.mvxRecyclerView1);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                recyclerView.SetLayoutManager(layoutManager);
                recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.LoadMoreTask, () => ViewModel.LoadMoreCommand);
            }

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }

            var menuBtn = FindViewById<ImageButton>(Resource.Id.menu);
            menuBtn.Click += delegate {
                DrawerLayout.OpenDrawer(GravityCompat.Start);
            };
        }
    }
}